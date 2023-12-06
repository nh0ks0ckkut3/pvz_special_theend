using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerData : MonoBehaviour
{
  public void setAll(string _id, string email, string username, string ingame, int age, string gender, int level, DetailCard[] listDetailCard)
  {
    PlayerPrefs.SetString("_id", _id);
    PlayerPrefs.SetString("email", email);
    PlayerPrefs.SetString("username", username);
    PlayerPrefs.SetString("ingame", ingame);
    PlayerPrefs.SetInt("age", age);
    PlayerPrefs.SetString("gender", gender);
    PlayerPrefs.SetInt("level", level);
    string json = JsonConvert.SerializeObject(listDetailCard);
    PlayerPrefs.SetString("listDetailCard", json);
    PlayerPrefs.Save();
  }
  public void setLevel()
  {
    int currentLevel = PlayerPrefs.GetInt("level", 1);
    int upLevel = currentLevel + 1;
    PlayerPrefs.SetInt("level", upLevel);
    PlayerPrefs.Save();
    // goi api updatelevel
  }
  public int loadLevel()
  {
    return PlayerPrefs.GetInt("level", 1); // Giá trị mặc định là 0 nếu không có giá trị được lưu trữ trước đó
  }
  public string load_id()
  {
    return PlayerPrefs.GetString("_id", "");
  }  public string loadEmail()
  {
    return PlayerPrefs.GetString("email", "");
  }  public string loadUsername()
  {
    return PlayerPrefs.GetString("username", "");
  }  public string loadIngame()
  {
    return PlayerPrefs.GetString("ingame", "");
  }  public int loadAge()
  {
    return PlayerPrefs.GetInt("age", 0);
  }  public string loadGender()
  {
    return PlayerPrefs.GetString("gender", "");
  }
  public void buyNewCard(DetailCard newDetailCard)
  {
    DetailCard[] currentListDetailCard = loadListDetailCardVip();

    // Tạo một mảng mới có kích thước lớn hơn để chứa card mới
    DetailCard[] updatedList = new DetailCard[currentListDetailCard.Length + 1];

    // Copy dữ liệu từ mảng cũ sang mảng mới
    for (int i = 0; i < currentListDetailCard.Length; i++)
    {
      updatedList[i] = currentListDetailCard[i];
    }

    // Thêm card mới vào cuối mảng
    updatedList[currentListDetailCard.Length] = newDetailCard;

    // Lưu mảng mới vào PlayerPrefs
    string json = JsonConvert.SerializeObject(updatedList);
    PlayerPrefs.SetString("listDetailCard", json);
    PlayerPrefs.Save();
    // goi api them 1 detailcardvip
  }
  public DetailCard[] loadListDetailCardVip()
  {
    string json = PlayerPrefs.GetString("listDetailCard", "[]");
    if (!string.IsNullOrEmpty(json))
    {
      return JsonConvert.DeserializeObject<DetailCard[]>(json);
    }
    else
    {
      return new DetailCard[0]; // Trả về mảng rỗng nếu không có dữ liệu
    }
  }
}

