using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ChatBubbleManager : MonoBehaviour
{
    public static ChatBubbleManager _i;


    public static ChatBubbleManager i
    {
        get
        {

            if (_i == null) _i = Instantiate(Resources.Load<ChatBubbleManager>("ChatBubbleManager "));

            return _i;

        }
    }



        public Transform pfChatBubble;
}
