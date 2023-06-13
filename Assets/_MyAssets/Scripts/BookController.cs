using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class BookController : MonoBehaviour
{
    [SerializeField]
    private List<Material> photos = new List<Material>(); //list of materials; each material has a texture of a single photo
    private int currentPage = 0; //NB: each page has 2 photos

    [Header("Pages reference")]
    [SerializeField]
    private GameObject firstPage_1;
    [SerializeField]
    private GameObject centralPage_2;
    [SerializeField]
    private GameObject centralPage_3;
    [SerializeField]
    private GameObject lastPage_4;

    [Header("UI Buttons reference")]
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Button previousButton;

    [SerializeField]
    private Animator bookAnimatorController;

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

    private bool isLastPhoto()
    {
        return currentPage >= (photos.Count / 2 - 1);
    }

    private bool isFirstPhoto()
    {
        return currentPage == 0;
    }

    public void NextPage()
    {
        if (isLastPhoto()) { return; }
        if (!bookAnimatorController.GetCurrentAnimatorStateInfo(0).IsName(Constant.IDLE_ANIM))
        {
            //one animation is already playing
            return;
        }

        //set the indexes
        int indexActualFirstPhoto = currentPage * 2;
        int indexActualSecondPhoto = indexActualFirstPhoto + 1;
        int indexNewFirstPhoto = indexActualSecondPhoto + 1;
        int indexNewSecondPhoto = indexNewFirstPhoto + 1;
        
        //change materials of each page:
        //firstPage_1 and centralPage_2 are the pages visible when the animation of page change starts
        //centralPage_3 and lastPage_4 are the pages visible after the animation of page change
        firstPage_1.GetComponent<MeshRenderer>().material = photos[indexActualFirstPhoto];
        centralPage_2.GetComponent<MeshRenderer>().material = photos[indexActualSecondPhoto];
        centralPage_3.GetComponent<MeshRenderer>().material = photos[indexNewFirstPhoto];
        lastPage_4.GetComponent<MeshRenderer>().material = photos[indexNewSecondPhoto];

        //Play animation
        bookAnimatorController.Play(Constant.NEXT_PAGE_ANIM);

        //update index
        currentPage++;

        //if, after updating the index, we are at last photo, deactivate next button
        //or, if we are at first photo, deactivate previous button
        //the check is on both buttons in order to reactivate them after a previous deactivation
        nextButton.interactable = !isLastPhoto();
        previousButton.interactable = !isFirstPhoto();
    }

    public void PreviousPage()
    {
        if (isFirstPhoto()) { return; }
        if (!bookAnimatorController.GetCurrentAnimatorStateInfo(0).IsName(Constant.IDLE_ANIM))
        {
            //one animation is already playing
            return;
        }

        //set the indexes
        int indexActualFirstPhoto = currentPage * 2;
        int indexActualSecondPhoto = indexActualFirstPhoto + 1;
        int indexNewFirstPhoto = indexActualFirstPhoto - 1;
        int indexNewSecondPhoto = indexNewFirstPhoto - 1;
        

        //change materials of each page:
        //firstPage_1 and centralPage_2 are the pages visible when the animation of page change starts
        //centralPage_3 and lastPage_4 are the pages visible after the animation of page change
        firstPage_1.GetComponent<MeshRenderer>().material = photos[indexNewSecondPhoto];
        centralPage_2.GetComponent<MeshRenderer>().material = photos[indexNewFirstPhoto];
        centralPage_3.GetComponent<MeshRenderer>().material = photos[indexActualFirstPhoto];
        lastPage_4.GetComponent<MeshRenderer>().material = photos[indexActualSecondPhoto];

        //Play animation
        bookAnimatorController.Play(Constant.PREVIOUS_PAGE_ANIM);

        //update index
        currentPage--;

        //if, after updating the index, we are at first photo, deactivate previous button
        //or, if we are at last photo, deactivate next button
        //the check is on both buttons in order to reactivate them after a previous deactivation
        previousButton.interactable = !isFirstPhoto();
        nextButton.interactable = !isLastPhoto();
    }
}
