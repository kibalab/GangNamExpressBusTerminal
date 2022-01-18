using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class K13A_ScreenEditor : ShaderGUI
{
    public bool baseGUIopen = false;
    public bool normalGUIopen = true;
    public bool AdvancedGUIopen = false;

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {

        var style = new GUIStyle();
        style = GUI.skin.box;
        style.normal.background = MakeTex(2, 2, new Color(0, 0, 0, 0.2f));

        Material targetMat = materialEditor.target as Material;

        GUILayout.Box(properties[1].textureValue, GUILayout.ExpandWidth(true), GUILayout.Height(300));

        // see if redify is set, and show a checkbox
        EditorGUILayout.BeginHorizontal(style);

        EditorGUILayout.BeginVertical();
        materialEditor.TexturePropertySingleLine(new GUIContent("Render Texture", "Video"), properties[1]);

        if (GUILayout.Button("General Settings")) normalGUIopen = !normalGUIopen;
        if (normalGUIopen)
        {
            EditorGUILayout.BeginHorizontal(style);
            EditorGUILayout.LabelField("GI Lightness", GUILayout.Width(100));
            properties[3].floatValue = EditorGUILayout.Slider(properties[3].floatValue, 0, 30);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal(style);
            EditorGUILayout.LabelField("Gamma Weight", GUILayout.Width(100));
            properties[4].floatValue = EditorGUILayout.Slider(properties[4].floatValue, 0, 10);
            EditorGUILayout.EndHorizontal();

            materialEditor.TexturePropertySingleLine(new GUIContent("Overlay Texture"), properties[0]);
            materialEditor.TexturePropertySingleLine(new GUIContent("Mask Texture"), properties[5]);
        }

        EditorGUILayout.Space(20);



        if (GUILayout.Button("Advanced Settings")) AdvancedGUIopen = !AdvancedGUIopen;

        EditorGUILayout.BeginVertical(style);

        if (AdvancedGUIopen)
        {
            materialEditor.TexturePropertySingleLine(new GUIContent("Pixel Map"), properties[6]);

            var scale = EditorGUILayout.Vector2Field("Scale", targetMat.GetTextureScale("_PixelMask"));
            foreach (var property in properties)
            {
                if (property.name.Equals("_PixelMask"))
                {
                    property.textureScaleAndOffset = (new Vector4(scale.x, scale.y, scale.x, scale.y));
                }
            }

            EditorGUILayout.BeginHorizontal(style);
            EditorGUILayout.LabelField("Fade Axis Range", GUILayout.Width(100));
            properties[7].floatValue = EditorGUILayout.Slider(properties[7].floatValue, 0, 10);
            EditorGUILayout.EndHorizontal();
        }


        EditorGUILayout.EndVertical();


        bool redify = Array.IndexOf(targetMat.shaderKeywords, "APPLY_GAMMA") != -1;
        EditorGUI.BeginChangeCheck();
        redify = EditorGUILayout.Toggle("Apply Gamma", redify);
        if (EditorGUI.EndChangeCheck())
        {
            // enable or disable the keyword based on checkbox
            if (redify)
                targetMat.EnableKeyword("APPLY_GAMMA");
            else
                targetMat.DisableKeyword("APPLY_GAMMA");
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();



        EditorGUILayout.Space(20);

        EditorGUILayout.BeginVertical(style);
        baseGUIopen = EditorGUILayout.BeginFoldoutHeaderGroup(baseGUIopen, "Base Inspector");

        if (baseGUIopen)
        {

            base.OnGUI(materialEditor, properties);

        }

        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.EndVertical();
    }
}
