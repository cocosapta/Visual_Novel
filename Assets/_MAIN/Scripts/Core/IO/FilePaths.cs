using UnityEngine;

public class FilePaths
{
   private const string HOME_DIRECTORY_SYMBOL = "~/";
   public static readonly string root = $"{Application.dataPath}/gameData/";

   public static readonly string resources_graphics = "Graphics/";
   public static readonly string resources_backgroundImages = $"{resources_graphics}BG Images/";
   public static readonly string resources_backgroundVideo = $"{resources_graphics}BG Video/";
   public static readonly string resources_blendTextures = $"{resources_graphics}Transition Effects/";

   public static readonly string resources_audio = "Audio/";
   public static readonly string resources_sfx = $"{resources_audio}SFX/";
   public static readonly string resources_voices = $"{resources_audio}Voices/";
   public static readonly string resources_music = $"{resources_audio}Music/";
   public static readonly string resources_ambience = $"{resources_audio}Ambience/";

   public static string GetPathToResource(string defaultPath, string resourcesName)
   {
      if (resourcesName.StartsWith(HOME_DIRECTORY_SYMBOL))
         return resourcesName.Substring(HOME_DIRECTORY_SYMBOL.Length);

      return defaultPath + resourcesName;
   }
}
