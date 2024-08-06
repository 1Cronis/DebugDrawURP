using System.Collections.Generic;
using UnityEngine;

namespace CustomDebugDraw
{
    public static class MaterialPullComponent
    {
        private static Material material = new Material(Shader.Find("Custom/CustomRender"));
        private static List<Material> UpdateMaterialsBuffer = new();
        private static List<Material> FixedUpdateMaterialsBuffer = new();

        private static int countMaterialUpdate = 0;
        private static int countMaterialFixedUpdate = 0;

        //TODO есть проблемма с очень большим созданием листа // проблемма в том что на каждый элемет меша
        //даю свой материал 

        public static Material GetMaterial(Color color)
        {
            Material material;

            if (Time.deltaTime == Time.fixedDeltaTime)
                material = AddMaterialFixedUpdate();
            else
                material = AddMaterialUpdate();

            material.color = color;


            return material;
        }

        public static Material AddMaterialUpdate()
        {
            var count = countMaterialUpdate;
          
            if (UpdateMaterialsBuffer.Count == count)
            {
                UpdateMaterialsBuffer.Add(new Material(material));
                countMaterialUpdate++;
               
            }
            else
                countMaterialUpdate++;

            if (UpdateMaterialsBuffer[count] == null)
            {
                UpdateMaterialsBuffer[count] = new Material(material);
            }

            return UpdateMaterialsBuffer[count];
        }

        public static Material AddMaterialFixedUpdate()
        {
            var count = countMaterialFixedUpdate;
           
            if (FixedUpdateMaterialsBuffer.Count == count)
            {
                FixedUpdateMaterialsBuffer.Add(new Material(material));
                countMaterialFixedUpdate++;
            }
            else
                countMaterialFixedUpdate++;

            if (FixedUpdateMaterialsBuffer[count] == null)
            {
                FixedUpdateMaterialsBuffer[count] = new Material(material);
            }
           
            return FixedUpdateMaterialsBuffer[count];
        }

        public static void UpdateListMaterialUpdate()
        {
            for (int i = countMaterialUpdate; i < UpdateMaterialsBuffer.Count; i++)
            {
                Object.Destroy(UpdateMaterialsBuffer[i]);
                UpdateMaterialsBuffer.RemoveAt(i);
            }

            countMaterialUpdate = 0;

        }

        public static void UpdateListMaterialFixedUpdate()
        {
            for (int i = countMaterialFixedUpdate; i < FixedUpdateMaterialsBuffer.Count; i++)
            {
                Object.Destroy(FixedUpdateMaterialsBuffer[i]);
                FixedUpdateMaterialsBuffer.RemoveAt(i);
            }

            countMaterialFixedUpdate = 0;
           
        }
    }
}