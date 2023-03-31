const modal = document.querySelector('.modall')
const openModalBtns = document.querySelectorAll('.open_btn')
const iconCloseModal = document.querySelector('.modal__headerr i')
const buttonCloseModal = document.querySelector('.close')

function toggleModal() {
	modal.classList.toggle('an')
}

openModalBtns.forEach(openModalBtn => openModalBtn.addEventListener('click', toggleModal))
iconCloseModal.addEventListener('click', toggleModal)
buttonCloseModal.addEventListener('click', toggleModal)

//modal.addEventListener('click', (e) => {
//	if (e.target == e.currentTarget) toggleModal()
//})
