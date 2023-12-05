using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ResetForgetPassWord : MonoBehaviour
{
  public TMP_InputField edtEmail;
  public TMP_Text txtError;
  public GameObject panelOTP, paneSendEmail;
  public void senEmail()
  {
    var email = edtEmail.text;

    ResetForgetRequest resetForgetRequest = new ResetForgetRequest(email);
    ResetPass(resetForgetRequest);
    StartCoroutine(ResetPass(resetForgetRequest));
  }

  IEnumerator ResetPass(ResetForgetRequest resetForgetRequest)
  {
    string jsonStringRequest = JsonConvert.SerializeObject(resetForgetRequest);

    var request = new UnityWebRequest("https://api-plantsvszombie-bason-694aafc26756.herokuapp.com/users/forgotPasswordAPP", "POST");
    byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
    request.uploadHandler = new UploadHandlerRaw(bodyRaw); // gửi dữ liệu lên server
    request.downloadHandler = new DownloadHandlerBuffer(); // nhận dữ liệu từ server trả về
    request.SetRequestHeader("Content-Type", "application/json");
    // khi gửi dữ liệu lên server, hàm này sẽ tạm dừng trong khi gửi dữ liệu thành công hoặc có lỗi
    yield return request.SendWebRequest();

    if (request.result != UnityWebRequest.Result.Success)
    {
      txtError.text = "có lỗi xảy ra thử lại sau";
      Debug.Log(txtError.text);
    }
    else
    {
      // trả về dữ liệu phản hồi của server
      var jsonString = request.downloadHandler.text.ToString();
      // chuyển đổi kiểu Json thành một đối tượng C#
      ResetPassReponse resetPassReponse = JsonConvert.DeserializeObject<ResetPassReponse>(jsonString);
      if (resetPassReponse.status != true)
      {
        //tài khoản không đúng
        txtError.text = "Gửi email không thành công";
        Debug.Log(resetPassReponse.message);
      }
      else
      {
        txtError.text = "Gửi email thành công";
        //SceneManager.LoadScene("");
        panelOTP.SetActive(true);
        paneSendEmail.SetActive(false);
        Debug.Log(resetPassReponse.message);
      }
    }
    request.Dispose();
  }
}
