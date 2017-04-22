// **********************************************************************
// 
// 文件名(File Name)：             SlipTexture
// 
// 作者(Author)：                  Sheen
// 
// 创建时间(CreateTime):           2015/11/23 13:38:24
//
// 网址：                          www.youke.pro
//
// **********************************************************************

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class SlipTexture : EditorWindow
{
    public Texture2D MainTex = null;

    [MenuItem("Window/分割图集")]
    public static void CreateWindows()
    {
        SlipTexture sp = EditorWindow.CreateInstance<SlipTexture>();
        sp.titleContent = new GUIContent("分割图集");
        sp.Show(true);
    }

    void OnGUI()
    {
        MainTex = EditorGUILayout.ObjectField("mainText", MainTex, typeof(Texture2D)) as Texture2D;
        if (MainTex != null)
        {

            if (GUILayout.Button("导出成小图片"))
            {
                string path = AssetDatabase.GetAssetPath(MainTex);
                TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
                if (importer.textureType != TextureImporterType.Sprite ||
                    importer.spriteImportMode != SpriteImportMode.Multiple ||
                    importer.spritesheet.Length == 0
                    )
                {
                    Debug.LogError("当前图片不是Sprite Multiple格式 或者没有分割");
                    return;
                }
                importer.isReadable = true;
                AssetDatabase.ImportAsset(path);
                AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);

                string savepath = EditorUtility.OpenFolderPanel("选择要保存的文件夹", Application.dataPath, "");
                if (!string.IsNullOrEmpty(savepath))
                {
                    foreach (SpriteMetaData metaData in importer.spritesheet)//遍历小图集
                    {
                        Texture2D myimage = new Texture2D((int)metaData.rect.width, (int)metaData.rect.height);
                        for (int y = (int)metaData.rect.y; y < metaData.rect.y + metaData.rect.height; y++)//Y轴像素
                        {
                            for (int x = (int)metaData.rect.x; x < metaData.rect.x + metaData.rect.width; x++)
                                myimage.SetPixel(x - (int)metaData.rect.x, y - (int)metaData.rect.y, MainTex.GetPixel(x, y));
                        }


                        //转换纹理到EncodeToPNG兼容格式
                        if (myimage.format != TextureFormat.ARGB32 && myimage.format != TextureFormat.RGB24)
                        {
                            Texture2D newTexture = new Texture2D(myimage.width, myimage.height);
                            newTexture.SetPixels(myimage.GetPixels(0), 0);
                            myimage = newTexture;
                        }
                        byte[] pngData = myimage.EncodeToPNG();


                        //AssetDatabase.CreateAsset(myimage, rootPath + "/" + image.name + "/" + metaData.name + ".PNG");
                        File.WriteAllBytes(savepath + "/" + metaData.name + ".PNG", pngData);
                    }
                }
            }
        }
    }
}
