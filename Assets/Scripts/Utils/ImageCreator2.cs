using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageCreator : MonoBehaviour
{
    public List<Sprite> hairs,heads,bodys;
    int ID = 0;
    public SpriteRenderer Hair, Head, Body;
    public string saveFolderPath = "Assets/SavedImages"; // A pasta onde você deseja salvar a imagem
    public string imageName;
    private void OnEnable()
    {
        //CaptureAndSaveCameraView();
    }
    void Start()
    {
        CaptureAndSaveCameraView();
        //// Certifique-se de que a pasta de salvamento exista, senão crie-a
        //if (!System.IO.Directory.Exists(saveFolderPath))
        //{
        //    System.IO.Directory.CreateDirectory(saveFolderPath);
        //}

        //foreach(Sprite hair in hairs)
        //{
        //    for (float colorH2 = 0; colorH2 < 360; colorH2 += 36)
        //    {
        //        Body.sprite = bodys[0];
        //        Color color2 = Color.HSVToRGB(colorH2 / 360, 0.8f, 0.8f);
        //        Body.color = color2;
        //        foreach (Sprite head in heads)
        //        {
        //            Head.sprite = head;
        //            for (float colorH = 0; colorH < 360; colorH += 36)
        //            {
        //                Hair.sprite = hair;
        //                Color color = Color.HSVToRGB(colorH / 360, 0.8f, 0.6f);
        //                Hair.color = color;
        //                CaptureAndSaveCameraView();
        //            }
        //        }
        //    }
        //}
    }


   public  void CaptureAndSaveCameraView()
    {
        // Obtenha a textura da visão da câmera principal
        Texture2D screenshotTexture = CaptureCameraViewToTexture();

        // Encode a textura como um arquivo PNG
        byte[] bytes = screenshotTexture.EncodeToPNG();

        // Salvar o PNG em um arquivo
        string savePath = System.IO.Path.Combine(saveFolderPath, $"{imageName}{ID}.png");
        System.IO.File.WriteAllBytes(savePath, bytes);
        ID++;
    }

    Texture2D CaptureCameraViewToTexture()
    {
        // Obtenha a visão da câmera principal como uma textura
        RenderTexture renderTexture = new RenderTexture(Screen.width,Screen.height, 24);
        Camera.main.targetTexture = renderTexture;
        Camera.main.Render();

        // Crie uma textura a partir do RenderTexture
        Texture2D texture = new Texture2D(Screen.width, Screen.height);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();

        // Limpe os recursos
        Camera.main.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        return texture;

    }
}
