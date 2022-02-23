/*
*	TickLuck
*	All rights reserved
*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterZone : MonoBehaviour
{
    #region Fields
    [SerializeField] public GameObject DoneUI;
    [SerializeField] public Text points_txt;
    [SerializeField] private Material water_material_spoiled;
    public Material water_material;
    [SerializeField] public List<WaterPart> water_parts;
    [SerializeField] public List<GameObject> water_points;

    public delegate void MoveWater();
    public event MoveWater mw;

    private int points_counter;

    [Header("For Development")]
    [Space]
    public int out_value;
    #endregion

    #region Unity Methods
    void Start()
    {
        DoneUI.SetActive(false);
        points_counter = water_points.Count;
        out_value = 0;
        DefaultPointText();

        foreach (var item in water_parts)
        {
            item.GetComponent<Renderer>().material = water_material_spoiled;
        }
    }
    #endregion

    public void DefaultPointText()
    {
        points_txt.text = "0/" + points_counter.ToString();
    }

    public void ModifyPointsText(int new_number)
    {
        out_value += new_number;

        //if (out_value < 0 || out_value > water_points.Count)
        //    return;

        points_txt.text = out_value.ToString() + "/" + points_counter.ToString();
    }

    //public void MoveWaterPartsUp()
    //{
    //    if (mw != null)
    //        mw();
    //}
}
