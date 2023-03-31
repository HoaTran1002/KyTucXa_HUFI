
const video_ = document.getElementById('body_categories-video');
const video_title = document.getElementById('body_categories-video-title');
const settingImg = document.querySelector('.header-top-right_has_login-img');

window.onscroll = function() {scrollFunction()};
$(document).ready(function(){
    $('.body_categories-video-list').slick({
        infinite: true,
        slidesToShow: 2,
        slidesToScroll: 2,
        dots:true
    });
  });

function getSrcVid(){
    var items = document.querySelectorAll('.body_categories-video-item')
    items.forEach(element => {
        element.onclick=()=>{
            var code = element.getAttribute('data-video').split('v=');
            video_.src ='https://www.youtube.com/embed/'+ code[1];
            var name =element.children[1].innerText
            video_title.innerText = name ;
        }
    });
}
function clickImgSetting(){
    settingImg.addEventListener("click",()=>{
        settingImg.classList.toggle("active")
    })
}
function scrollFunction() {
    if (document.body.scrollTop > 300 || document.documentElement.scrollTop > 300) {
        document.querySelector(".scroll_up-logo").style.display = "flex";
    } else {
        document.querySelector(".scroll_up-logo").style.display = "none";
    }
}
function dropDownList(){
    var dropDownListTabletMobile=document.querySelectorAll('.model-main-menu-item-icon')
    var contentListTabletMobile = document.querySelectorAll('.model-main-submenu-list')
    dropDownListTabletMobile.forEach((e,i) => {
        e.addEventListener("click",()=>{
            contentListTabletMobile[i].classList.toggle('active');
        })
    });
}
function dorpDownSearch(){
    var btnSearch =document.querySelector('.header-top-left_input-search-mobile');
    var headerSearchMobile=document.querySelector('.header-search-mobile');
    btnSearch.addEventListener("click",e=>{
        headerSearchMobile.classList.toggle('active')
        })
}
dorpDownSearch();
dropDownList();
clickImgSetting();
getSrcVid();