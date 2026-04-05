using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
   private void OnTriggerExit2D(Collider2D collision)
   {
      if (!collision.CompareTag("Area"))
            return;
      
      Vector3 playerPos=Gamemanager.instance.player.transform.position;
      Vector3 myPos=transform.position;
      float differnetX=Mathf.Abs(playerPos.x-myPos.x);
      float differnetY=Mathf.Abs(playerPos.y-myPos.y);

      Vector3 playerDir = Gamemanager.instance.player.inputVector;
      float dirX = playerDir.x < 0 ? -1 : 1;
      float dirY = playerDir.y < 0 ? -1 : 1;

      switch (transform.tag)
      {
          case"Ground":
              if (differnetX > differnetY)
              {
                  transform.Translate(Vector3.right*dirX *40);
              }
              else if(differnetY > differnetX)
              {
                  transform.Translate(Vector3.up*dirY *40);
              }
              
              break;
          case "Enemy":

              break;
          
      }
      

   }
}

