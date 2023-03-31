const toggleSwitch = document.querySelector('.sidebar-right-info-topic-wraper input[type="checkbox"]');
const currentTheme = localStorage.getItem('theme');
const timeHour_header= document.querySelector('.time-today .hour')
const timeMinute_header= document.querySelector('.time-today .minute')
const dayToday_numberOfDay= document.querySelector('.day-today .number-day')
const dayToday_day= document.querySelector('.day-today .day')
const dayToday_month= document.querySelector('.day-today .month')
const dayToday_year= document.querySelector('.day-today .year')
const sidebar_day =document.querySelector('.sidebar-right-info-dayOfWeek-title .month span')
const sidebar_daysOfWeek = document.querySelectorAll('.sidebar-right-info-dayOfWeek-content .week .dayOfWeek ')
const sidebar_daysOfWeek_day = document.querySelectorAll('.sidebar-right-info-dayOfWeek-content .week .dayOfWeek .day')
const sidebar_daysOfWeek_active = document.querySelector('.sidebar-right-info-dayOfWeek-content .week .dayOfWeek.selected')
if (currentTheme) {
    document.documentElement.setAttribute('data-theme', currentTheme);
    if (currentTheme === 'dark') {
        toggleSwitch.checked = true;
    }
}
function switchTheme(e) {
    if (e.target.checked) {
        document.documentElement.setAttribute('data-theme', 'dark');
        localStorage.setItem('theme', 'dark');
    } else {
        document.documentElement.setAttribute('data-theme', 'light');
        localStorage.setItem('theme', 'light');
    }
}
toggleSwitch.addEventListener('change', switchTheme);

function displayDateTime() {
    var today = new Date();
    var daysOfThisWeek = [];
    var daysOfWeek = ['Chủ nhật', 'Thứ hai', 'Thứ ba', 'Thứ tư', 'Thứ năm', 'Thứ sáu', 'Thứ bảy']
    var dayOfWeek = daysOfWeek[today.getDay()]
    var currentDay = today.getDay();
    var day = today.getDate()
    var month = today.getMonth() + 1
    var year = today.getFullYear() 
    var hour = today.getHours()
    var minute = today.getMinutes();
    for (var i = 0; i < 7; i++) {
        var day_of = new Date();
        day_of.setDate(today.getDate() - currentDay + i);
        daysOfThisWeek.push(day_of.getDate());
    }
    timeHour_header.innerHTML = hour
    timeMinute_header.innerHTML = minute
    dayToday_numberOfDay.innerHTML = dayOfWeek
    dayToday_day.innerHTML = day
    dayToday_month.innerHTML = month
    dayToday_year.innerHTML = year
    sidebar_day.innerHTML = month

    sidebar_daysOfWeek.forEach((e,index)=>{
        if(currentDay === index ){
            e.classList.add('selected')  
        } else if(currentDay !== index){
            e.classList.remove('selected')
        }
    })

    sidebar_daysOfWeek_day.forEach((e,index)=>{
        e.innerHTML = daysOfThisWeek[index]
    })


  }
  
  setInterval(displayDateTime, 1000);
  
