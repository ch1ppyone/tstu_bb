using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCoolDown : MonoBehaviour
{

      [SerializeField] private Transform _buttonsContainer;

      public void CoolDown()
      {
          StartCoroutine(CreateCoolDown());
      }
      public void EnbaleAll()
      {
            for (var i = 0; i < _buttonsContainer.childCount; i++) 
                _buttonsContainer.GetChild(i).GetComponent<Button>().interactable = true;
      }
      
      public void DisabelAll()
      {
          for (var i = 0; i < _buttonsContainer.childCount; i++) 
              _buttonsContainer.GetChild(i).GetComponent<Button>().interactable = false;
      }

      private IEnumerator CreateCoolDown()
      {
          DisabelAll();
          yield return new WaitForSeconds(1);
          EnbaleAll();
      }
      
      
}
