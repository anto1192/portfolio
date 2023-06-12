using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class BookController : MonoBehaviour
{
    [SerializeField]
    private List<Material> photos = new List<Material>(); //list of materials; each material has a texture of a single photo
    public int currentPage = 0; //NB: each page has 2 photos

    [Header("Pages reference")]
    public GameObject firstPage_1;
    public GameObject centralPage_2;
    public GameObject centralPage_3;
    public GameObject lastPage_4;

    [SerializeField]
    private Animator bookAnimatorController;

    private const string PREVIOUS_PAGE_ANIM = "PreviousPage";
    private const string NEXT_PAGE_ANIM = "NextPage";
    private const string IDLE_ANIM = "Idle";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextPage();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            PreviousPage();
        }
    }

    public void NextPage()
    {
        if (currentPage >= (photos.Count/2 - 1)) {
            Debug.Log("Last photo");
            return;
        }
        if (!bookAnimatorController.GetCurrentAnimatorStateInfo(0).IsName(IDLE_ANIM))
        {
            //one animation is already playing
            return;
        }

        int indexActualFirstPhoto = currentPage * 2;
        int indexActualSecondPhoto = indexActualFirstPhoto + 1;
        int indexNewFirstPhoto = indexActualSecondPhoto + 1;
        int indexNewSecondPhoto = indexNewFirstPhoto + 1;
        currentPage++;

        firstPage_1.GetComponent<MeshRenderer>().material = photos[indexActualFirstPhoto];
        centralPage_2.GetComponent<MeshRenderer>().material = photos[indexActualSecondPhoto];
        centralPage_3.GetComponent<MeshRenderer>().material = photos[indexNewFirstPhoto];
        lastPage_4.GetComponent<MeshRenderer>().material = photos[indexNewSecondPhoto];

        //Play animation
        bookAnimatorController.Play(NEXT_PAGE_ANIM);
    }

    public void PreviousPage()
    {
        if (currentPage == 0)
        {
            Debug.Log("First photo");
            return;
        }
        if (!bookAnimatorController.GetCurrentAnimatorStateInfo(0).IsName(IDLE_ANIM))
        {
            //one animation is already playing
            return;
        }

        int indexActualFirstPhoto = currentPage * 2;
        int indexActualSecondPhoto = indexActualFirstPhoto + 1;
        int indexNewFirstPhoto = indexActualFirstPhoto - 1;
        int indexNewSecondPhoto = indexNewFirstPhoto - 1;
        currentPage--;

        firstPage_1.GetComponent<MeshRenderer>().material = photos[indexNewSecondPhoto];
        centralPage_2.GetComponent<MeshRenderer>().material = photos[indexNewFirstPhoto];
        centralPage_3.GetComponent<MeshRenderer>().material = photos[indexActualFirstPhoto];
        lastPage_4.GetComponent<MeshRenderer>().material = photos[indexActualSecondPhoto];

        //Play animation
        bookAnimatorController.Play(PREVIOUS_PAGE_ANIM);      
    }
}
