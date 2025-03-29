using UnityEngine;

public class NightModeSwitcher : MonoBehaviour
{

    public Light directionalLight;    
    public Material nightSkybox;    
    public Color nightAmbientColor = new Color(0.1f, 0.1f, 0.2f);  
    public float nightLightIntensity = 0.2f;  

    public void SwitchToNightMode()
    {
   
        RenderSettings.skybox = nightSkybox;
        
      
        RenderSettings.ambientLight = nightAmbientColor;
        
     
        if (directionalLight != null)
        {
            directionalLight.intensity = nightLightIntensity;
            directionalLight.color = new Color(0.6f, 0.6f, 1.0f); 
        }
        
        DynamicGI.UpdateEnvironment();
    }
}
