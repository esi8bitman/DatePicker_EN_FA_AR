using UnityEngine;
using UnityEngine.UI;

public class DatePicker_Ctrl : MonoBehaviour
{
    public static DatePicker_Ctrl datePicker;
    enum Mode { Christian, Persian, Hijri };
    [SerializeField]
    Mode mode;
    public Text text;

    void Awake(){
        datePicker = this;
    }

    public void ShowDatePicker(){
        transform.GetChild((int)mode).gameObject.SetActive(true);
        text.text = "---";
    }

}
