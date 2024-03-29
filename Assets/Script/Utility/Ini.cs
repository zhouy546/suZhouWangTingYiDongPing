﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Ini : MonoBehaviour {
    private ReadJson readJson;

    private DealWithUDPMessage dealWithUDPMessage;

    private GetUDPTouch getUDPTouch;

    private GetUDPMessage getUDPMessage;

    private void  Start() {
         StartCoroutine(initialization());
    }

    IEnumerator initialization()
    {

        dealWithUDPMessage = FindObjectOfType<DealWithUDPMessage>();

        readJson = FindObjectOfType<ReadJson>();

        getUDPTouch = FindObjectOfType<GetUDPTouch>();

        getUDPMessage = FindObjectOfType<GetUDPMessage>();

        yield return StartCoroutine(readJson.initialization());

        yield return StartCoroutine( getUDPMessage.InitializationUdp());

        yield return StartCoroutine(   getUDPTouch.InitializationUdp());

        yield return StartCoroutine(dealWithUDPMessage.Initialization());

        yield return new WaitForSeconds(0f);
    }


    IEnumerator GetSpriteListFromStreamAsset(string path, string suffix, List<Sprite> sprites)
    {
        List<Texture> texturesList = new List<Texture>();
        string streamingPath = Application.streamingAssetsPath + path;
        DirectoryInfo dir = new DirectoryInfo(streamingPath);

        GetAllFiles(dir, path, suffix);

        foreach (string de in jpgName)
        {
            WWW www = new WWW(Application.streamingAssetsPath + path + de);
            yield return www;
            if (www != null)
            {
                Texture texture = www.texture;
                texture.name = de;
                texturesList.Add(texture);
            }
            if (www.isDone)
            {
                www.Dispose();
            }
        }

        foreach (var texture in texturesList)
        {
            Sprite sprite = Sprite.Create(texture as Texture2D, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            sprite.name = texture.name;
            sprites.Add(sprite);
        }

        jpgName.Clear();
    }

    List<string> jpgName = new List<string>();

    public void GetAllFiles(DirectoryInfo dir, string Filepath, string suffix)
    {
        FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();   //初始化一个FileSystemInfo类型的实例
        foreach (FileSystemInfo i in fileinfo)              //循环遍历fileinfo下的所有内容
        {
            if (i is DirectoryInfo)             //当在DirectoryInfo中存在i时
            {
                GetAllFiles((DirectoryInfo)i, Filepath, suffix);  //获取i下的所有文件
            }
            else
            {
                string str = i.FullName;        //记录i的绝对路径
                string path = Application.streamingAssetsPath + Filepath;
                string strType = str.Substring(path.Length);

                if (strType.Substring(strType.Length - 3).ToLower() == suffix)
                {
                    if (jpgName.Contains(strType))
                    {
                    }
                    else
                    {
                        jpgName.Add(strType);
                    }
                }
            }
        }
    }
}
