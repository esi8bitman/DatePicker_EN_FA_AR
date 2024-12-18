using System;
using UnityEngine;
using UnityEngine.UI;

public class GregorianDatePicker_Ctrl : MonoBehaviour
{
    [SerializeField]
    Text txt_date,txt_CurrentMonth,txt_CurrentYear;
    [SerializeField]
    Transform daysParent,yearsParent;
    [SerializeField]
    Color[] sessions_Colors;
    RawImage img_Date;    
    DateTime date;
    int i,firstDayOfMonth,remindDays,currentDay,currentMonth,currentYear,currentDecade;
    String[] Months = new string[]{"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
    // Start is called before the first frame update
    void Start()
    {
        img_Date = GetComponent<RawImage>();
        Reset();
        //print($"{currentYear}/{currentMonth}/{currentDay}");
        currentDecade = (currentYear/10)*10;
        for(i=0;i<10;i++)
            yearsParent.GetChild(i).GetChild(0).GetComponent<Text>().text = $"{currentDecade+i}";
    }

    void SetDaysFrame(){
        txt_CurrentMonth.text = Months[currentMonth-1];
       
        date = new DateTime(currentYear,currentMonth,1);//get first of month day
        firstDayOfMonth = (int)date.DayOfWeek;
               
        for(i=0;i<firstDayOfMonth;i++) daysParent.GetChild(i).gameObject.SetActive(false);
        for(i=firstDayOfMonth;i<DateTime.DaysInMonth(currentYear,currentMonth)+firstDayOfMonth;i++){
            int day = i-(firstDayOfMonth-1);
            daysParent.GetChild(i).gameObject.SetActive(true);
            daysParent.GetChild(i).GetChild(1).GetComponent<Text>().text = $"{i-(firstDayOfMonth-1)}";
            daysParent.GetChild(i).GetComponent<Button>().onClick.AddListener(()=>SetDay(day));

            daysParent.GetChild(i).GetChild(0).gameObject.SetActive(currentYear == DateTime.Now.Year && currentMonth == DateTime.Now.Month && day == DateTime.Now.Day);
        }
            remindDays = i;
        for(i=remindDays;i<daysParent.childCount;i++) daysParent.GetChild(i).gameObject.SetActive(false);
    }

    public void ChangeMonthPage(int value){
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

    public void SetDay(int value){ //final
        currentDay = value;
        date = new DateTime(currentYear,currentMonth,currentDay);

        DatePicker_Ctrl.datePicker.text.text = $"{currentYear}/{currentMonth}/{currentDay}\n{date.DayOfWeek}  {currentDay}  {date.ToString("MMMM")}";
        gameObject.SetActive(false);
    }
    public void SetMonth(int value){
        currentMonth = value+1;
        txt_CurrentMonth.text = Months[currentMonth-1];
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
        currentYear = DateTime.Now.Year;
        currentMonth = DateTime.Now.Month;
        currentDay = DateTime.Now.Day;
        txt_CurrentYear.text = $"{currentYear}";
        SetDaysFrame();
        SetBKColor();
        txt_date.text = $"{currentYear}/{currentMonth}/{currentDay}";
    }

    void SetBKColor(){
        //NOTE : Use it for show sessions!
        if (currentMonth >= 3 && currentMonth <= 5)//Spring
            img_Date.color = sessions_Colors[0];
        else if (currentMonth >= 6 && currentMonth <= 8) //Summer
            img_Date.color = sessions_Colors[1];
        else if (currentMonth >= 9 && currentMonth <= 11) //Fall
            img_Date.color = sessions_Colors[2];
        else if (currentMonth == 12 || currentMonth == 2 || currentMonth == 1) //Winter
            img_Date.color = sessions_Colors[3];
    }

}
