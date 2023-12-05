using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPassReponse
{
    public ResetPassReponse(string message)
    {
        this.message = message;
    }

    public ResetPassReponse(bool status)
    {
      this.status = status;
    }

    public string message;
    public bool status;
}
