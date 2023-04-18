const imageInput = document.getElementById('imageInput');
const preview = document.getElementById('previewImage');
const saveProfile = document.querySelector('.profile--form__submit-btn')
const editProfile = document.querySelector('.profile--form__unsubmit-btn')
const textFields = document.querySelectorAll('.profile--form__text-field input')
const textFieldsSelect = document.querySelectorAll('.profile--form__text-field select')

function editProfileImg() {
    if (imageInput != null || preview != null) {
        imageInput.addEventListener('change', () => {
            const file = imageInput.files[0];
            const reader = new FileReader();

            reader.addEventListener('load', () => {
                preview.src = reader.result;
            }, false);

            if (file) {
                reader.readAsDataURL(file);
            }
        });
    }

}


function editProfileInfo() {
    if (editProfile != null || saveProfile != null) {
        editProfile.onclick = () => {
            textFields.forEach(element => {
                element.classList.add('active')
            });
            textFieldsSelect.forEach(element => {
                element.classList.add('active')
            });
            if (imageInput != null) {
                imageInput.classList.add('active')
            }
            editProfile.classList.add('edit-active')
            saveProfile.classList.add('save-active')
        }
    }
}
function saveProfileInfo() {
    if (saveProfile != null || editProfile != null) {
        saveProfile.onclick = () => {
            textFields.forEach(element => {
                element.classList.remove('active')
            });
            textFieldsSelect.forEach(element => {
                element.classList.remove('active')
            });
            if (imageInput != null) {
                imageInput.classList.remove('active')
            }

            editProfile.classList.remove('edit-active')
            saveProfile.classList.remove('save-active')
        }
    }

}
editProfileInfo();
saveProfileInfo();
editProfileImg();