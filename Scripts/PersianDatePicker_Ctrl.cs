using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class PersianDatePicker_Ctrl : MonoBehaviour
{
    [SerializeField]
    Text txt_date,txt_CurrentMonth,txt_CurrentYear;
    [SerializeField]
    Transform daysParent,yearsParent;
    [SerializeField]
    Color[] sessions_Colors;
    RawImage img_Date;
    PersianCalendar calendar = new PersianCalendar();
    DateTime date;
    int i,firstDayOfMonth,remindDays,currentDay,currentMonth,currentYear,currentDecade = 1400;
    string[] PersianMonths  = new string[]{"ﻦﯾﺩﺭﻭﺮﻓ", "ﺖﺸﻬﺒﯾﺩﺭﺍ", "ﺩﺍﺩﺮﺧ", "ﺮﯿﺗ", "ﺩﺍﺩﺮﻣ", "ﺭﻮﯾﺮﻬﺷ", "ﺮﻬﻣ", "ﻥﺎﺑﺁ", "ﺭﺫﺁ", "ىﺩ", "ﻦﻤﻬﺑ", "ﺪﻨﻔﺳﺍ"},
    PersianDays = new string[]{"ﻪﻌﻤﺟ" ,"ﻪﺒﻨﺸﺠﻨﭘ", "ﻪﺒﻨﺷﺭﺎﻬﭼ" ,"ﻪﺒﻨﺷ", "ﻪﺳ ﻪﺒﻨﺷﻭﺩ" ,"ﻪﺒﻨﺸﻜﯾ", "ﻪﺒﻨﺷ"};
    // Start is called before the first frame update
    void Start()
    {
        img_Date = GetComponent<RawImage>();
        Reset();
        currentDecade = (currentYear/10)*10;
        for(i=0;i<10;i++)
            yearsParent.GetChild(i).GetChild(0).GetComponent<Text>().text = $"{currentDecade+i}";
        //print($"{currentYear}/{currentMonth}/{currentDay}");
    }

    void SetDaysFrame(){
        txt_CurrentMonth.text = PersianMonths[currentMonth-1];
       
        date = new DateTime(currentYear,currentMonth,1,calendar);//get first of month day
        firstDayOfMonth = (int)calendar.GetDayOfWeek(date);
        firstDayOfMonth = firstDayOfMonth == 6 ? 0 : firstDayOfMonth + 1;
               
        for(i=0;i<firstDayOfMonth;i++) daysParent.GetChild(i).gameObject.SetActive(false);
        for(i=firstDayOfMonth;i<calendar.GetDaysInMonth(currentYear,currentMonth)+firstDayOfMonth;i++){
            int day = i-(firstDayOfMonth-1);
            daysParent.GetChild(i).gameObject.SetActive(true);
            daysParent.GetChild(i).GetChild(1).GetComponent<Text>().text = $"{i-(firstDayOfMonth-1)}";
            daysParent.GetChild(i).GetComponent<Button>().onClick.AddListener(()=>SetDay(day));

            daysParent.GetChild(i).GetChild(0).gameObject.SetActive(currentYear == calendar.GetYear(DateTime.Now) && currentMonth == calendar.GetMonth(DateTime.Now) && day == calendar.GetDayOfMonth(DateTime.Now));
        }
            remindDays = i;
        for(i=remindDays;i<daysParent.childCount;i++) daysParent.GetChild(i).gameObject.SetActive(false);
    }

    public void ChangeMonth(int value){
        currentMonth+= value;
        if(currentMonth<1){
            currentMonth = 12;
            currentYear--;
        }else if(currentMonth>12){
            currentMonth = 1;
            currentYear++;
        }
        SetDaysFrame();
        SetBKColor();
        txt_date.text = $"{currentYear}/{currentMonth}/{currentDay}";
    }

    public void SetDay(int value){
        currentDay = value;
        DatePicker_Ctrl.datePicker.text.text = $"{currentYear}/{currentMonth}/{currentDay}\n{PersianMonths[currentMonth-1]}  {currentDay}  {PersianDays[(int)DateTime.Now.DayOfWeek]}";
        gameObject.SetActive(false);
    }
    public void SetMonth(int value){
        currentMonth = value+1;
        txt_CurrentMonth.text = PersianMonths[currentMonth-1];
        txt_date.text = $"{currentYear}/{currentMonth}/{currentDay}";
        SetBKColor();
    }

    public void ChangeYearPage(bool next){
        currentDecade += next?10:-10; 
        for(i=0;i<10;i++)
            yearsParent.GetChild(i).GetChild(0).GetComponent<Text>().text = $"{currentDecade+i}";
    }

    public void SetYear(int value){
        currentYear = currentDecade + value;
        txt_CurrentYear.text = $"{currentYear}";
        txt_date.text = $"{currentYear}/{currentMonth}/{currentDay}";
    }

    public void Reset(){
        currentYear = calendar.GetYear(DateTime.Now);
        currentMonth = calendar.GetMonth(DateTime.Now);
        currentDay = calendar.GetDayOfMonth(DateTime.Now);
        txt_date.text = $"{currentYear}/{currentMonth}/{currentDay}";
        txt_CurrentYear.text = $"{currentYear}";
        SetDaysFrame();
        SetBKColor();
        txt_date.text = $"{currentYear}/{currentMonth}/{currentDay}";
    }

    void SetBKColor(){
                //NOTE : Use it for show sessions!
        if (currentMonth >= 1 && currentMonth <= 3)//Spring
            img_Date.color = sessions_Colors[0];
        else if (currentMonth >= 4 && currentMonth <= 6) //Summer
            img_Date.color = sessions_Colors[1];
        else if (currentMonth >= 7 && currentMonth <= 9) //Fall
            img_Date.color = sessions_Colors[2];
        else if (currentMonth >= 10 && currentMonth <= 12) //Winter
            img_Date.color = sessions_Colors[3];
    }

}
