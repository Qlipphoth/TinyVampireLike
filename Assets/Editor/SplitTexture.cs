// SplitTexture.cs

using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// 将图片分离成多张小图
/// </summary>
public class SplitTexture
{
    [MenuItem("Tools/SplitTexture")]
    static void DoSplitTexture()
    {
        // 获取所选图片
        Texture2D selectedImg = Selection.activeObject as Texture2D;
        string rootPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(selectedImg));
        string path = rootPath + "/" + selectedImg.name + ".png";
        TextureImporter texImp = AssetImporter.GetAtPath(path) as TextureImporter;
        // 设置为可读
        texImp.isReadable = true;
        AssetDatabase.ImportAsset(path);

        // 创建文件夹
        AssetDatabase.CreateFolder(rootPath, selectedImg.name);


        foreach (SpriteMetaData metaData in texImp.spritesheet)
        {
            var width = (int)metaData.rect.width;
            var height = (int)metaData.rect.height;
            Texture2D smallImg = new Texture2D(width, height);
            var pixelStartX = (int)metaData.rect.x;
            var pixelEndX = pixelStartX + width;
            var pixelStartY = (int)metaData.rect.y;
            var pixelEndY = pixelStartY + height;
            for (int x = pixelStartX; x <= pixelEndX; ++x)
            {
                for (int y = pixelStartY; y <= pixelEndY; ++y)
                {
                    smallImg.SetPixel(x - pixelStartX, y - pixelStartY, selectedImg.GetPixel(x, y));
                }
            }

            //  转换纹理到EncodeToPNG兼容格式
            if (TextureFormat.ARGB32 != smallImg.format  && TextureFormat.RGB24 != smallImg.format)
            {
                Texture2D img = new Texture2D(smallImg.width, smallImg.height);
                img.SetPixels(smallImg.GetPixels(0), 0);
                smallImg = img;
            }

            // 保存小图文件
            string smallImgPath = rootPath + "/" + selectedImg.name + "/" + metaData.name + ".png";
            File.WriteAllBytes(smallImgPath, smallImg.EncodeToPNG());
            // 刷新资源窗口界面
            AssetDatabase.Refresh();
            // 设置小图的格式
            TextureImporter smallTextureImp = AssetImporter.GetAtPath(smallImgPath) as TextureImporter;
            // 设置为可读
            smallTextureImp.isReadable = true;
            // 设置alpha通道
            smallTextureImp.alphaIsTransparency = true;
            // 不开启mipmap
            smallTextureImp.mipmapEnabled = false;
            AssetDatabase.ImportAsset(smallImgPath);
        }
    }
}
