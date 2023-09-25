using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform viewPortTransform;
    public RectTransform contentPanelTransform;
    public VerticalLayoutGroup VLG;

    public RectTransform[] potentialItems;
    private RectTransform[] ItemList;

    private float threshold;
    private float startingY;

    Vector2 OldVelocity;
    bool isUpdated;

    public bool spinning = false;

    private static int[] slot = {0, 3, 6, 1, 2, 4, 5, 4, 3, 6, 0, 1, 3, 5, 2, 6, 0, 1, 2, 4, 5};
//-374.5
    public IEnumerator SpinTo(int targetIndex) {
        float spinTime = 3f;
        while (spinTime > 0.0f) {
            spinTime -= Time.deltaTime;
            scrollRect.velocity = new Vector3(0, -15000, 0);
            yield return null;
        }
        SnapTo(targetIndex);
        spinning = false;
    }

    void SnapTo(int targetIndex)
    {
        scrollRect.velocity = Vector3.zero;
        contentPanelTransform.localPosition = new Vector3(contentPanelTransform.localPosition.x, startingY + (260 * ((ItemList.Length ) - targetIndex)) - 260, contentPanelTransform.localPosition.z);
        // contentPanelTransform.localPosition = new Vector3(contentPanelTransform.localPosition.x, startingY, contentPanelTransform.localPosition.z);

        // StartCoroutine(ArriveToTarget(targetIndex));
    }

    // dest = (ItemList[0].rect.height + VLG.spacing) * targetIndex + startingY;

    // IEnumerator ArriveToTarget(int targetIndex) {
    //     scrollRect.velocity = Vector3.zero;
    //     Vector3 dest = new Vector3(contentPanelTransform.localPosition.x, (ItemList[0].rect.height + VLG.spacing) * targetIndex + startingY, contentPanelTransform.localPosition.z);
    //     while (Vector3.Distance(contentPanelTransform.localPosition, dest) > 5) {
    //         Vector3 interpPos = Vector3.Lerp(contentPanelTransform.localPosition, dest, 50f);
    //         contentPanelTransform.localPosition = interpPos;
            
    //         if (Vector3.Distance(contentPanelTransform.localPosition, dest) <= 5) {
    //             contentPanelTransform.localPosition = dest;
    //         }
    //         yield return null;
    //     }
    //     spinning = false;
    // }


    // Start is called before the first frame update
    void Start()
    {
        generateItems();

        OldVelocity = Vector2.zero;
        isUpdated = false;

        int ItemsToAdd  = ItemList.Length;
        for (int i = 0; i < ItemsToAdd; i++) {
            RectTransform RT = Instantiate(ItemList[i % ItemList.Length], contentPanelTransform);
            RT.SetAsLastSibling();
        }

        for (int i = 0; i < ItemsToAdd; i++) {
            int num = ItemList.Length - i - 1;
            while (num < 0) {
                num += ItemList.Length;
            }
            RectTransform RT = Instantiate(ItemList[num], contentPanelTransform);
            RT.SetAsFirstSibling();
        }

        contentPanelTransform.localPosition -= new Vector3(contentPanelTransform.localPosition.x,
            (ItemList[0].rect.height + VLG.spacing) * ItemsToAdd,
            contentPanelTransform.localPosition.z);
        threshold = (ItemList[0].rect.height + VLG.spacing) * ItemsToAdd;
        startingY = contentPanelTransform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isUpdated) {
            isUpdated = false;
            scrollRect.velocity = OldVelocity;
        }

        if (contentPanelTransform.localPosition.y > startingY + threshold) {
            Canvas.ForceUpdateCanvases();
            OldVelocity = scrollRect.velocity;
            contentPanelTransform.localPosition -= new Vector3(0, ItemList.Length * (ItemList[0].rect.height + VLG.spacing), 0);
            isUpdated = true;
        }

        if (contentPanelTransform.localPosition.y < startingY - threshold) {
            Canvas.ForceUpdateCanvases();
            OldVelocity = scrollRect.velocity;
            contentPanelTransform.localPosition += new Vector3(0, ItemList.Length * (ItemList[0].rect.height + VLG.spacing), 0);
            isUpdated = true;
        }
    }

    private void generateItems() {
        ItemList = new RectTransform[slot.Length];
        for (int i = 0; i < slot.Length; i++) {
            RectTransform RT = Instantiate(potentialItems[slot[i]], contentPanelTransform);
            RT.gameObject.SetActive(true);
            ItemList[i] = RT;
        }
    }
}
