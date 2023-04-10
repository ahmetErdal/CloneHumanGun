using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using HumanGunCase.Managers;
public class WeaponSpecs : MonoBehaviour
{
    #region Singleton
    public static WeaponSpecs instance = null;
    private void Awake()
    {
        if (instance == null) instance = this;
        isPistol = true;
    }
    #endregion
    public int playerCount=0;
    
    public static string[] poseNames = { "Pose0", "Pose1", "Pose2", "Pose3" };
    #region All References
    [Header("Player")]
    [SerializeField] GameObject dummyGO;
    [SerializeField] float firstDummyEvolveGunTime;
    [SerializeField] float evolveGunTime;
    public List<Transform> gunsDummy = new List<Transform>();
    public List<Transform> riffleDummy = new List<Transform>();
    public List<Transform> shootgunDummy = new List<Transform>();
    public List<Transform> grenadeDuumy = new List<Transform>();
    [Space]
    [Header("Guns Position")]
    [SerializeField] List<Transform> pistolReferencePosition = new List<Transform>();
    [SerializeField] List<Transform> riffleReferencePosition = new List<Transform>();
    [SerializeField] List<Transform> shootgunReferencePosition = new List<Transform>();
    [SerializeField] List<Transform> grenadeReferencePosition = new List<Transform>();
    
    [Header("Bools")]
    [HideInInspector] public bool isPistol;
    [HideInInspector] public bool isRiffle;
    [HideInInspector] public bool isShoutgun;
    [HideInInspector] public bool isGrenade;
    #endregion

    //Pool yapýlacak
    private void Start()
    {
        StartConfigreGun();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollactableDummy"))
        {
            Destroy(other.GetComponent<CapsuleCollider>());
            Destroy(other.GetComponent<Rigidbody>());
            transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = false;
            pistolReferencePosition[0].GetChild(1).gameObject.SetActive(true);
            AddDummyForGuns(other.gameObject);
            EvolveForGuns();
        }
        if (other.gameObject.CompareTag("Money"))
        {
            other.gameObject.GetComponent<Money>().SetCollect();
            UIManager.instance.UpdateMoneyText();
        }
        if (other.gameObject.CompareTag("Door"))
        {
            other.GetComponent<Collider>().enabled = false;
            other.transform.parent.DOMoveY(-4, 1f);
            other.GetComponent<Door>().StartCalculate();
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            CheckFail();
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            GameManager.instance.isFinished = true;
        }
        if (other.gameObject.CompareTag("Barrel"))
        {
            CheckFail();
        }
    }
    public void AddDummyForGuns(GameObject dummy)
    {
        playerCount++;
        if (playerCount < 6)
        {
            gunsDummy.Add(dummy.transform);
            GameObject refGo = pistolReferencePosition[playerCount].gameObject;

            dummy.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = refGo.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
            dummy.transform.parent = pistolReferencePosition[playerCount].transform;
            dummy.transform.DOLocalMove(Vector3.zero, firstDummyEvolveGunTime);
            dummy.transform.DOLocalRotate(Vector3.zero, firstDummyEvolveGunTime);
            string pose = pistolReferencePosition[playerCount ].GetComponent<DummyPose>().poseName;
            
            dummy.GetComponent<Animator>().SetTrigger(pose);
            
        }
        else if (playerCount >= 6 && playerCount < 11)
        {
            riffleDummy.Add(dummy.transform);
            
            GameObject refGo = riffleReferencePosition[playerCount].gameObject;

            dummy.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = refGo.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
            dummy.transform.parent = refGo.transform;
            dummy.transform.DOLocalMove(Vector3.zero, firstDummyEvolveGunTime);
            dummy.transform.DOLocalRotate(Vector3.zero, firstDummyEvolveGunTime);
            string pose = refGo.GetComponent<DummyPose>().poseName;

            dummy.GetComponent<Animator>().SetTrigger(pose);
            
        }
        else if (playerCount>=11&&playerCount<20)
        {
            shootgunDummy.Add(dummy.transform);
            GameObject refGo = shootgunReferencePosition[playerCount].gameObject;

            dummy.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = refGo.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
            dummy.transform.parent = refGo.transform;
            dummy.transform.DOLocalMove(Vector3.zero, firstDummyEvolveGunTime);
            dummy.transform.DOLocalRotate(Vector3.zero, firstDummyEvolveGunTime);
            string pose = refGo.GetComponent<DummyPose>().poseName;

            dummy.GetComponent<Animator>().SetTrigger(pose);
            //shootgunReferencePosition[playerCount-1].transform.GetChild(1).gameObject.SetActive(true); ;
        }
        else if (playerCount>=20&&playerCount<30)
        {
            grenadeDuumy.Add(dummy.transform);
            GameObject refGo = grenadeReferencePosition[playerCount].gameObject;

            dummy.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = refGo.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
            dummy.transform.parent = refGo.transform;
            dummy.transform.DOLocalMove(Vector3.zero, firstDummyEvolveGunTime);
            dummy.transform.DOLocalRotate(Vector3.zero, firstDummyEvolveGunTime);
            string pose = refGo.GetComponent<DummyPose>().poseName;
            //grenadeReferencePosition[playerCount-1].transform.GetChild(1).gameObject.SetActive(true); ;
        }
        
    }
    public void ReducationForGuns()
    {
        if (playerCount < 6)
        {
            pistolReferencePosition[0].GetChild(1).gameObject.SetActive(true);
            List<Transform> temporalList = riffleDummy;
            int count = riffleDummy.Count;
            for (int i = 0; i < count; i++)
            {
                gunsDummy.Add(temporalList[i]);
                //temp.Remove(temporalList[i]);
                GameObject refGo = riffleReferencePosition[i].gameObject;
                temporalList[i].transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = refGo.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
                temporalList[i].transform.parent = refGo.transform;
                temporalList[i].transform.DOLocalMove(Vector3.zero, evolveGunTime);
                temporalList[i].transform.DOLocalRotate(Vector3.zero, evolveGunTime);
                string pose = refGo.GetComponent<DummyPose>().poseName;

                temporalList[i].GetComponent<Animator>().SetTrigger(pose);
            }
            BoolController(true, false, false, false);
        }
        else if (playerCount < 11)
        {
            riffleReferencePosition[0].GetChild(1).gameObject.SetActive(true);
            List<Transform> temporalList = shootgunDummy;
            int count = shootgunDummy.Count;
            for (int i = 0; i < count; i++)
            {

                Debug.Log("//" + i + " " + gunsDummy.Count);
                riffleDummy.Add(temporalList[i]);
                //temp.Remove(temporalList[i]);
                GameObject refGo = riffleReferencePosition[i].gameObject;
                temporalList[i].transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = refGo.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
                temporalList[i].transform.parent = refGo.transform;
                temporalList[i].transform.DOLocalMove(Vector3.zero, evolveGunTime);
                temporalList[i].transform.DOLocalRotate(Vector3.zero, evolveGunTime);
                string pose = refGo.GetComponent<DummyPose>().poseName;

                temporalList[i].GetComponent<Animator>().SetTrigger(pose);
            }

            BoolController(false, true, false, false);
        }
        else if (playerCount < 21)
        {
            shootgunReferencePosition[0].GetChild(1).gameObject.SetActive(true);
            List<Transform> temporalList = grenadeDuumy;
            int count = grenadeDuumy.Count;
            for (int i = 0; i < count; i++)
            {
                shootgunDummy.Add(temporalList[i]);

                GameObject refGo = shootgunReferencePosition[i].gameObject;
                temporalList[i].transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = refGo.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
                temporalList[i].transform.parent = refGo.transform;
                temporalList[i].transform.DOLocalMove(Vector3.zero, evolveGunTime);
                temporalList[i].transform.DOLocalMove(Vector3.zero, evolveGunTime);
                string pose = refGo.GetComponent<DummyPose>().poseName;

                temporalList[i].GetComponent<Animator>().SetTrigger(pose);
            }
            BoolController(false, false, true, false);
        }
       
    }
    public void EvolveForGuns()
    {
        if (playerCount < 6)
        {
            BoolController(true, false, false, false);
        }
        else if (playerCount ==6)
        {
            riffleReferencePosition[0].GetChild(1).gameObject.SetActive(true);
            List<Transform> temporalList = gunsDummy;
            int count = gunsDummy.Count;
            for (int i = 0; i <count; i++)
            {
               
                Debug.Log("//"+i+" "+gunsDummy.Count);
                riffleDummy.Add(temporalList[i]);
                //temp.Remove(temporalList[i]);
                GameObject refGo = riffleReferencePosition[i].gameObject;
                gunsDummy[i].transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = refGo.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
                temporalList[i].transform.parent = refGo.transform;
                temporalList[i].transform.DOLocalMove(Vector3.zero, evolveGunTime);
                temporalList[i].transform.DOLocalRotate(Vector3.zero, evolveGunTime);
                string pose = refGo.GetComponent<DummyPose>().poseName;

                temporalList[i].GetComponent<Animator>().SetTrigger(pose);
            }

            BoolController(false, true, false, false);
        }
        else if (playerCount==11)
        {
            shootgunReferencePosition[0].GetChild(1).gameObject.SetActive(true);
            List<Transform> temporalList = riffleDummy;
            int count = riffleDummy.Count;
            for (int i = 0; i < count; i++)
            {
                shootgunDummy.Add(temporalList[i]);
              
                GameObject refGo = shootgunReferencePosition[i ].gameObject;
                temporalList[i].transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = refGo.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
                temporalList[i].transform.parent = refGo.transform;
                temporalList[i].transform.DOLocalMove(Vector3.zero, evolveGunTime);
                temporalList[i].transform.DOLocalMove(Vector3.zero, evolveGunTime);
                string pose = refGo.GetComponent<DummyPose>().poseName;

                temporalList[i].GetComponent<Animator>().SetTrigger(pose);
            }
            BoolController(false, false, true, false);
        }
        else if (playerCount ==21)
        {
            grenadeReferencePosition[0].GetChild(1).gameObject.SetActive(true);
            List<Transform> temporalList = shootgunDummy;
            int count = shootgunDummy.Count;
            for (int i = 0; i < count; i++)
            {
                grenadeDuumy.Add(temporalList[i]);
                //shootgunDummy.Remove(shootgunDummy[i]);
                GameObject refGo = grenadeReferencePosition[i].gameObject;
                temporalList[i].transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = refGo.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
                temporalList[i].transform.parent = refGo.transform;
                temporalList[i].transform.DOLocalMove(Vector3.zero, evolveGunTime);
                temporalList[i].transform.DOLocalRotate(Vector3.zero, evolveGunTime);
                string pose = refGo.GetComponent<DummyPose>().poseName;

                temporalList[i].GetComponent<Animator>().SetTrigger(pose);
            }
            BoolController(false, false, false, true);
        }
       
    }

    void BoolController(bool pistol, bool riffle, bool shoutgun, bool grenade)
    {
        isPistol = pistol;
        isRiffle = riffle;
        isShoutgun = shoutgun;
        isGrenade = grenade;
    }
    
    void CheckFail()
    {
        if (playerCount>1)
        {
            if (isPistol)
            {
                playerCount --;
                Transform lastDummy = gunsDummy[gunsDummy.Count-1];
                lastDummy.parent = null;
                gunsDummy.Remove(lastDummy);
                lastDummy.transform.DOJump(transform.position + new Vector3(0, 0, 3), 2f, 1, .5f);
                //lastDummy.gameObject.AddComponent<Rigidbody>();

            }
            else if (isRiffle)
            {
                playerCount--;
                Transform lastDummy = riffleDummy[riffleDummy.Count-1];
                lastDummy.parent = null;
                riffleDummy.Remove(lastDummy);
                lastDummy.transform.DOJump(transform.position + new Vector3(0, 0, 3), 2f, 1, .5f);
                // lastDummy.gameObject.AddComponent<Rigidbody>();
            }
            else if (isShoutgun)
            {
                playerCount--;
                Transform lastDummy =shootgunDummy[shootgunDummy.Count-1];
                lastDummy.parent = null;
                shootgunDummy.Remove(lastDummy);
                lastDummy.transform.DOJump(transform.position + new Vector3(0, 0, 3), 2f, 1, .5f);
                //lastDummy.gameObject.AddComponent<Rigidbody>();
            }
            else if (isGrenade)
            {
                playerCount--;
                Transform lastDummy =grenadeDuumy[grenadeDuumy.Count-1];
                lastDummy.parent = null;
                grenadeDuumy.Remove(lastDummy);
                lastDummy.transform.DOJump(transform.position + new Vector3(0, 0, 3), 2f, 1, .5f);
                //lastDummy.gameObject.AddComponent<Rigidbody>();
            }
           ReducationForGuns();
        }
        else if(!GameManager.instance.isFinished)
        {
            Debug.Log("Bitti");
            UIManager.instance.PanelController(false, false, true, false);
            PlayerController.instance.maxForwardSpeed = 0;
            PlayerController.instance.maxHorizontalSpeed = 0;
           
        }
        else if(GameManager.instance.isFinished)
        {
            Debug.Log("Bitti Kazandýn");
            UIManager.instance.PanelController(false, false, false, true);
            PlayerController.instance.maxForwardSpeed = 0;
            PlayerController.instance.maxHorizontalSpeed = 0;
        }
    
    }
    public void IncreaseDummy()
    {
        GameObject newDummy = Instantiate(dummyGO, transform.position, Quaternion.identity);
       
        if (isPistol)
        {

            gunsDummy.Add(newDummy.transform);
            GameObject refGo = pistolReferencePosition[playerCount].gameObject;

            newDummy.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = refGo.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
            newDummy.transform.parent = pistolReferencePosition[playerCount].transform;
            newDummy.transform.DOLocalMove(Vector3.zero, firstDummyEvolveGunTime);
            newDummy.transform.DOLocalRotate(Vector3.zero, firstDummyEvolveGunTime);
            string pose = pistolReferencePosition[playerCount].GetComponent<DummyPose>().poseName;

            newDummy.GetComponent<Animator>().SetTrigger(pose);
            playerCount++;
        }
        else if(isRiffle)
        {
            riffleDummy.Add(newDummy.transform);
            GameObject refGo = riffleReferencePosition[playerCount].gameObject;

            newDummy.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = refGo.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
            newDummy.transform.parent = riffleReferencePosition[playerCount].transform;
            newDummy.transform.DOLocalMove(Vector3.zero, firstDummyEvolveGunTime);
            newDummy.transform.DOLocalRotate(Vector3.zero, firstDummyEvolveGunTime);
            string pose = riffleReferencePosition[playerCount].GetComponent<DummyPose>().poseName;

            newDummy.GetComponent<Animator>().SetTrigger(pose);
            playerCount++;
        }
        else if (isShoutgun)
        {
            shootgunDummy.Add(newDummy.transform);
            GameObject refGo = shootgunReferencePosition[playerCount].gameObject;

            newDummy.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = refGo.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
            newDummy.transform.parent = shootgunReferencePosition[playerCount].transform;
            newDummy.transform.DOLocalMove(Vector3.zero, firstDummyEvolveGunTime);
            newDummy.transform.DOLocalRotate(Vector3.zero, firstDummyEvolveGunTime);
            string pose = shootgunReferencePosition[playerCount].GetComponent<DummyPose>().poseName;

            newDummy.GetComponent<Animator>().SetTrigger(pose);
            playerCount++;
        }
        else if (isGrenade)
        {
            grenadeDuumy.Add(newDummy.transform);
            GameObject refGo = grenadeReferencePosition[playerCount].gameObject;

            newDummy.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = refGo.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
            newDummy.transform.parent = grenadeReferencePosition[playerCount].transform;
            newDummy.transform.DOLocalMove(Vector3.zero, firstDummyEvolveGunTime);
            newDummy.transform.DOLocalRotate(Vector3.zero, firstDummyEvolveGunTime);
            string pose = grenadeReferencePosition[playerCount].GetComponent<DummyPose>().poseName;

            newDummy.GetComponent<Animator>().SetTrigger(pose);
            playerCount++;
        }
    }
        
    public void StartConfigreGun()
    {
        int startDummy = IncramentalManager.instance.unitsCount;
        if (startDummy==0)
        {
            return;
        }
        
        GameObject startConfigreDummy = Instantiate(dummyGO, transform.position, Quaternion.identity);
        if (isPistol)
        {
            for (int i =1; i < startDummy+1; i++)
            {
                gunsDummy.Add(startConfigreDummy.transform);
                GameObject refGo = pistolReferencePosition[playerCount].gameObject;

                startConfigreDummy.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = refGo.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
                startConfigreDummy.transform.parent = pistolReferencePosition[playerCount].transform;
                startConfigreDummy.transform.DOLocalMove(Vector3.zero, 0f);
                startConfigreDummy.transform.DOLocalRotate(Vector3.zero, 0f);
                string pose = pistolReferencePosition[playerCount].GetComponent<DummyPose>().poseName;

                startConfigreDummy.GetComponent<Animator>().SetTrigger(pose);
                playerCount++;
            }
        }
        else if (isRiffle)
        {
            for (int i = 1; i < startDummy+1; i++)
            {
                riffleDummy.Add(startConfigreDummy.transform);
                GameObject refGo = riffleReferencePosition[playerCount].gameObject;

                startConfigreDummy.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = refGo.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
                startConfigreDummy.transform.parent = riffleReferencePosition[playerCount].transform;
                startConfigreDummy.transform.DOLocalMove(Vector3.zero, 0f);
                startConfigreDummy.transform.DOLocalRotate(Vector3.zero, 0f);
                string pose = riffleReferencePosition[playerCount].GetComponent<DummyPose>().poseName;

                startConfigreDummy.GetComponent<Animator>().SetTrigger(pose);
                playerCount++;
            }
        }
        else if (isShoutgun)
        {
            for (int i = 1; i < startDummy+1; i++)
            {

                shootgunDummy.Add(startConfigreDummy.transform);
                GameObject refGo = shootgunReferencePosition[playerCount].gameObject;

                startConfigreDummy.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = refGo.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
                startConfigreDummy.transform.parent = shootgunReferencePosition[playerCount].transform;
                startConfigreDummy.transform.DOLocalMove(Vector3.zero, 0f);
                startConfigreDummy.transform.DOLocalRotate(Vector3.zero, 0f);
                string pose = shootgunReferencePosition[playerCount].GetComponent<DummyPose>().poseName;

                startConfigreDummy.GetComponent<Animator>().SetTrigger(pose);
                playerCount++;
            }
        }
        else if (isGrenade)
        {
            for (int i = 1; i < startDummy+1; i++)
            {
                grenadeDuumy.Add(startConfigreDummy.transform);
                GameObject refGo = grenadeReferencePosition[playerCount].gameObject;

                startConfigreDummy.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = refGo.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
                startConfigreDummy.transform.parent = grenadeReferencePosition[playerCount].transform;
                startConfigreDummy.transform.DOLocalMove(Vector3.zero, 0f);
                startConfigreDummy.transform.DOLocalRotate(Vector3.zero, 0f);
                string pose = grenadeReferencePosition[playerCount].GetComponent<DummyPose>().poseName;

                startConfigreDummy.GetComponent<Animator>().SetTrigger(pose);
                playerCount++;
            }
        }
    }
}
