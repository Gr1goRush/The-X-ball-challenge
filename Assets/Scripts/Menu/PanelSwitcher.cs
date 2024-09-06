using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    public SwitchingPanel[] panels;

    int currentPanelIndex = -1;

    private void Start()
    {
        ShowPanel(0);    
    }

    public void ShowPanel(int index)
    {
        if (currentPanelIndex.Equals(index))
        {
            return;
        }

        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].gameObject.SetActive(i == index);
        }

        currentPanelIndex = index;
    }

    public void ShowPanel(string panelName)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            bool isCurrentPanel = panels[i].PanelName == panelName;
            panels[i].gameObject.SetActive(isCurrentPanel);
            if (isCurrentPanel)
            {
                currentPanelIndex = i;
            }
        }
    }

    public void HideAll()
    {
        ShowPanel(-1);
    }


    private void Reset()
    {
        panels = FindObjectsOfType<SwitchingPanel>();
    }
}
