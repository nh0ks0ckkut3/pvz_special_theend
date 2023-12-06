using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailCard
{
  // Start is called before the first frame update
  public DetailCard(string _id, string name, string detail, int __v, string price, string image, string cardVip_id, string user_id, string created_at)
  {
    this._id = _id;
    this.name = name;
    this.detail = detail;
    this.__v = __v;
    this.price = price;
    this.image = image;
    this.cardVip_id = cardVip_id;
    this.user_id = user_id;
    this.created_at = created_at;

  }

  public string _id { get; set; }
  public string name { get; set; }
  public string detail { get; set; }
  public string price { get; set; }
  public string image { get; set; }
  public string cardVip_id { get; set; }
  public int __v { get; set; }
  public string user_id { get; set; }
  public string created_at { get; set; }

}
