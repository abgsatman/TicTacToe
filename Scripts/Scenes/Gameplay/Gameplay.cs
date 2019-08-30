using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public Text NoticeText;
    
    // Start is called before the first frame update
    void Start()
    {
        NoticeText.text = RoomData.Instance.roomId;
    }

}
