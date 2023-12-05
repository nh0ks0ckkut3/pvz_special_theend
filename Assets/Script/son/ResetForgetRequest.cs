using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetForgetRequest : MonoBehaviour
{
  // Start is called before the first frame update
  public ResetForgetRequest(string email)
  {
    this.email = email;
  }

  public string email { get; set; }
}
