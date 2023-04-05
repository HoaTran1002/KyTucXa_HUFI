const mainTabs = document.querySelectorAll('.sidebar-right-content-titles-items')
const mainContent = document.querySelectorAll('.sidebar-right-content-tab')
const itemsMailboxOption = document.querySelectorAll('.mailbox-stdudent-item-option')
const itemsStudentOption = document.querySelectorAll('.list-student-setting')

itemsMailboxOption.forEach((e)=>{
    e.addEventListener('click',function(element){
        let itemMailboxOptionActive = document.querySelector('.mailbox-stdudent-item-option.active')
        if (itemMailboxOptionActive) {
            itemMailboxOptionActive.classList.remove('active')
        }
        element.target.parentNode.classList.toggle('active')
    })
})
itemsStudentOption.forEach((e)=>{
    e.addEventListener('click',function(element){
        const itemsStudentOptionActive = document.querySelector('.list-student-setting.active')
        if (itemsStudentOptionActive) {
            itemsStudentOptionActive.classList.remove('active')
        }
        element.target.parentNode.classList.toggle('active')
    })
})
mainTabs.forEach((element,i)=>{
    var index = i
    element.onclick = (e) =>{
        var mainTab = document.querySelector('.sidebar-right-content-titles-items.active')
        var mainContentActive = document.querySelector('.sidebar-right-content-tab.active')
        if(mainTabs){
            mainTab.classList.remove('active')
        }
        if(mainContent){
            mainContentActive.classList.remove('active')
        }
        e.target.classList.add('active')
        mainContent[index].classList.add('active')
    }
})

