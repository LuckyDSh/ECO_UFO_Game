/*
*	TickLuck
*	All rights reserved
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UFO_Controller : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject tree;
    [SerializeField] private GameObject[] rock;
    [SerializeField] private int tree_collection_size;
    [SerializeField] private GameObject Cone;
    [SerializeField] private Transform in_out_point;
    [SerializeField] List<HouseZone> HouseZones;
    [SerializeField] List<WaterZone> WaterZones;
    [SerializeField] List<StoneZone> StoneZones;
    [SerializeField] private float speed;
    [SerializeField] private float time_on_each_point;
    [SerializeField] private GameObject GO_UI;
    [SerializeField] private Button In_btn;
    [SerializeField] private Button Out_btn;
    [SerializeField] private Text tree_counter_txt;
    [SerializeField] private Text water_counter_txt;
    [SerializeField] private Text rock_counter_txt;

    public List<Resource> Tree_Collection;
    public List<Resource> Water_Collection;
    public List<Resource> Rock_Collection;
    private Resource tree_buffer;
    private Resource rock_buffer;
    private GameObject water_point_buffer;

    private HouseZone current_target_WOOD;
    private WaterZone current_target_WATER;
    private StoneZone current_target_STONE;
    private bool is_moving;
    public static Transform in_out_point_refference;

    // Wood Parts / Tree Configurations
    private int WoodPartsCOUNTER;
    private int TreeCollectionCOUNTER;
    private int TreePointsCOUNTER;
    private int TreeCOUNTER;
    private int TreeOutCOUNTER; // For Future Use

    // Water Parts / Water Configurations
    private int WaterPartsCOUNTER;
    private int WaterCollectionCOUNTER;
    private int WaterPointsCOUNTER;
    private int WaterCOUNTER;
    private int WaterOutCOUNTER; // For Future Use

    // Stone Parts / Rock Configurations
    private int StonePartsCOUNTER;
    private int RockCollectionCOUNTER;
    private int RockPointsCOUNTER;
    private int RockCOUNTER;
    private int RockOutCOUNTER; // For Future Use

    private Resource last_tree_buffer;
    private Resource last_water_buffer;
    private Resource last_rock_buffer;

    private int HouseZones_counter;
    private int WaterZones_counter;
    private int StoneZones_counter;

    // To Rare the Operation speed
    private bool is_operation_done;
    #endregion

    #region Unity Methods
    void Start()
    {
        HouseZones_counter = 1;
        WaterZones_counter = 1;
        StoneZones_counter = 1;

        Tree_Collection = new List<Resource>();
        Water_Collection = new List<Resource>();

        //tree_counter_txt.text = tree_collection_size.ToString();
        TreeCOUNTER = 0;
        WaterOutCOUNTER = 0;
        TreeOutCOUNTER = 0;
        tree_counter_txt.text = TreeCOUNTER.ToString();

        //InitializeTrees(tree_collection_size);

        In_btn.onClick.AddListener(() => In_WOOD());
        Out_btn.onClick.AddListener(() => Out_WOOD());

        Out_btn.gameObject.SetActive(false);
        in_out_point_refference = in_out_point;
        GO_UI.SetActive(false);
        current_target_WOOD = HouseZones[Random.Range(0, HouseZones.Count)];
        Cone.SetActive(false);
        is_moving = true;
    }

    void FixedUpdate()
    {
        //if (is_moving)
        //{
        //    // Move to the current Target
        //    if (Vector3.Distance(transform.position, current_target.transform.position) >= 0.05f)
        //        transform.position += (current_target.transform.position - transform.position) * Time.fixedDeltaTime * speed;
        //    else
        //    {
        //        is_moving = false;

        //        // Start In/Out Routine
        //        StartCoroutine(In_Out_Routine());
        //    }
        //}
    }
    #endregion

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "HouseZone")
        {
            current_target_WOOD = other.GetComponent<HouseZone>();
            StartCoroutine(InProcedure_WOOD());
        }

        if (other.tag == "WaterZone")
        {
            current_target_WATER = other.GetComponent<WaterZone>();
            In_WATER();
        }

        if (other.tag == "StoneZone")
        {
            current_target_STONE = other.GetComponent<StoneZone>();
            StartCoroutine(InProcedure_STONE());
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HouseZone" || other.tag == "WaterZone" || other.tag == "StoneZone")
        {
            if (HouseZones_counter + WaterZones_counter + StoneZones_counter == 0)
            {
                GO_UI.SetActive(true);
            }
        }
    }

    #region In_Out_Routine // Out of Date
    private IEnumerator In_Out_Routine()
    {
        Cone.SetActive(true);

        yield return new WaitForSeconds(time_on_each_point);

        In_btn.gameObject.SetActive(true);
        Out_btn.gameObject.SetActive(false);

        Cone.SetActive(false);
        HouseZones.Remove(current_target_WOOD);

        if (HouseZones.Count > 0)
        {
            current_target_WOOD = HouseZones[HouseZones.Count - 1];
            is_moving = true;
        }
        else
        {
            GO_UI.SetActive(true);
        }
    }
    #endregion; // Out of Date

    #region STONE

    #region InProcedure_STONE
    private IEnumerator InProcedure_STONE()
    {
        yield return new WaitForSeconds(1f);

        if (!is_operation_done)
        {
            In_STONE();

            is_operation_done = true;
        }
        else
            is_operation_done = false;
    }
    #endregion

    #region In_STONE
    public void In_STONE()
    {
        in_out_point_refference = in_out_point;

        StonePartsCOUNTER = current_target_STONE.stone_parts.Count;
        if (StonePartsCOUNTER > 0)
        {
            Out_btn.gameObject.SetActive(false);
            In_btn.gameObject.SetActive(true);

            current_target_STONE.stone_parts[StonePartsCOUNTER - 1].MoveToTarget();
            current_target_STONE.stone_parts.RemoveAt(StonePartsCOUNTER - 1);

            InitializeRocks(1);
            RockCOUNTER++; // Not here to ++
        }
        else
        {
            Out_STONE();

            Out_btn.gameObject.SetActive(true);
            In_btn.gameObject.SetActive(false);
        }
        rock_counter_txt.text = RockCOUNTER.ToString();
    }
    #endregion

    #region Out_STONE
    public void Out_STONE()
    {
        RockCollectionCOUNTER = Rock_Collection.Count;
        if (RockCollectionCOUNTER > 0)
        {
            RockPointsCOUNTER = current_target_STONE.rock_points.Count;
            if (RockPointsCOUNTER > 0)
            {
                last_rock_buffer = Rock_Collection[RockCollectionCOUNTER - 1];
                last_rock_buffer.transform.position = in_out_point.position;
                last_rock_buffer.gameObject.SetActive(true);
                last_rock_buffer.MoveToTarget(current_target_STONE.rock_points[RockPointsCOUNTER - 1].transform);
                Rock_Collection.Remove(last_rock_buffer);
                current_target_STONE.rock_points.RemoveAt(RockPointsCOUNTER - 1);

                current_target_STONE.ModifyPointsText(1);
                RockCOUNTER--;
            }
            else
            {
                StoneZones.Remove(current_target_STONE);
                StoneZones_counter = StoneZones.Count;
                current_target_STONE.DoneUI.SetActive(true);
            }
        }
    }
    #endregion

    #region InitializeRocks
    private void InitializeRocks(int number)
    {
        for (int i = 0; i < number; i++)
        {
            rock_buffer = Instantiate(rock[Random.Range(0, rock.Length)], in_out_point.position, in_out_point.rotation).GetComponent<Resource>();
            Rock_Collection.Add(rock_buffer);
            rock_buffer.gameObject.SetActive(false);
        }
    }
    #endregion

    #endregion

    #region WATER
    #region In_WATER
    public void In_WATER()
    {
        in_out_point_refference = in_out_point;

        WaterPartsCOUNTER = current_target_WATER.water_parts.Count;
        if (WaterPartsCOUNTER > 0)
        {
            Out_btn.gameObject.SetActive(false);
            In_btn.gameObject.SetActive(true);

            // Add Water here
            current_target_WATER.water_parts[WaterPartsCOUNTER - 1].MoveToTarget();
            current_target_WATER.water_parts.RemoveAt(WaterPartsCOUNTER - 1);

            // Here we Init new Water
            WaterCOUNTER++;
        }
        else
        {
            Out_WATER();

            Out_btn.gameObject.SetActive(true);
            In_btn.gameObject.SetActive(false);
        }
        water_counter_txt.text = WaterCOUNTER.ToString();
    }
    #endregion

    #region Out_WATER
    public void Out_WATER()
    {
        WaterCollectionCOUNTER = Water_Collection.Count;
        if (WaterCollectionCOUNTER > 0)
        {
            WaterPointsCOUNTER = current_target_WATER.water_points.Count;
            if (WaterPointsCOUNTER > 0)
            {
                last_water_buffer = Water_Collection[WaterCollectionCOUNTER - 1];
                last_water_buffer.enabled = true;

                last_water_buffer.transform.position = in_out_point.position;
                last_water_buffer.gameObject.SetActive(true);

                last_water_buffer.MoveToTarget(current_target_WATER.water_points[WaterPointsCOUNTER - 1].transform);
                last_water_buffer.GetComponent<Rigidbody>().isKinematic = false;
                last_water_buffer.GetComponent<Collider>().isTrigger = false;

                Water_Collection.RemoveAt(WaterCollectionCOUNTER - 1);


                // Last Point refference
                //water_point_buffer = current_target_WATER.water_points[WaterCollectionCOUNTER - 1];

                // Remove the Last Point and push it to the Front
                current_target_WATER.water_points.RemoveAt(WaterPointsCOUNTER - 1);
                //current_target_WATER.water_points.Insert(0, water_point_buffer);

                current_target_WATER.ModifyPointsText(1);
                WaterCOUNTER--;
            }
            else
            {
                WaterZones.Remove(current_target_WATER);
                WaterZones_counter = WaterZones.Count;
                current_target_WATER.DoneUI.SetActive(true);
            }
        }
    }
    #endregion
    #endregion

    #region WOOD
    private IEnumerator InProcedure_WOOD()
    {
        yield return new WaitForSeconds(1f);

        if (!is_operation_done)
        {
            In_WOOD();

            is_operation_done = true;
        }
        else
            is_operation_done = false;
    }

    #region In_WOOD
    public void In_WOOD()
    {
        in_out_point_refference = in_out_point;

        WoodPartsCOUNTER = current_target_WOOD.wood_parts.Count;
        if (WoodPartsCOUNTER > 0)
        {
            Out_btn.gameObject.SetActive(false);
            In_btn.gameObject.SetActive(true);

            current_target_WOOD.wood_parts[WoodPartsCOUNTER - 1].MoveToTarget();
            current_target_WOOD.wood_parts.RemoveAt(WoodPartsCOUNTER - 1);

            InitializeTrees(1);
            TreeCOUNTER++; // Not here to ++
        }
        else
        {
            Out_WOOD();

            Out_btn.gameObject.SetActive(true);
            In_btn.gameObject.SetActive(false);
        }
        tree_counter_txt.text = TreeCOUNTER.ToString();
    }
    #endregion

    #region Out_WOOD
    public void Out_WOOD()
    {
        TreeCollectionCOUNTER = Tree_Collection.Count;
        if (TreeCollectionCOUNTER > 0)
        {
            TreePointsCOUNTER = current_target_WOOD.tree_points.Count;
            if (TreePointsCOUNTER > 0)
            {
                last_tree_buffer = Tree_Collection[TreeCollectionCOUNTER - 1];
                last_tree_buffer.transform.position = in_out_point.position;
                last_tree_buffer.gameObject.SetActive(true);
                last_tree_buffer.MoveToTarget(current_target_WOOD.tree_points[TreePointsCOUNTER - 1].transform);
                Tree_Collection.Remove(last_tree_buffer);
                current_target_WOOD.tree_points.RemoveAt(TreePointsCOUNTER - 1);

                current_target_WOOD.ModifyPointsText(1);
                TreeCOUNTER--;
            }
            else
            {
                HouseZones.Remove(current_target_WOOD);
                HouseZones_counter = HouseZones.Count;
                current_target_WOOD.DoneUI.SetActive(true);
            }
        }
    }
    #endregion

    #region InitializeTrees
    private void InitializeTrees(int number)
    {
        for (int i = 0; i < number; i++)
        {
            tree_buffer = Instantiate(tree, in_out_point.position, in_out_point.rotation).GetComponent<Resource>();
            Tree_Collection.Add(tree_buffer);
            tree_buffer.gameObject.SetActive(false);
        }
    }
    #endregion
    #endregion
}
