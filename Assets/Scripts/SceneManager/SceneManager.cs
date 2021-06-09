using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SceneManager : BaseController
{
       private List<GameObject> _openedScenes = new List<GameObject>();

       public SceneManager()
       {
           BaseSceneController.CoreApplication = CoreApplication;
       }
       
       public async void Show(Scene scene, Level levelToOpen, List<AvailableElement> availableElement = null)
       {
           var obj = await Addressables.LoadAssetAsync<GameObject>(scene.ToString()).Task;
           var newObj = GameObject.Instantiate(obj, Vector3.zero, new Quaternion()).GetComponent<BaseSceneController>();
           newObj.LoadLevel(levelToOpen, availableElement);
           _openedScenes.Add(newObj.gameObject);
       }

       public void HideAll()
       {
           foreach (var openedScene in _openedScenes)
           {
               GameObject.Destroy(openedScene);
           } 
           
           _openedScenes.Clear();
       }
}

public enum Scene
{
       Editor,
       PlayMode
}