///////////добавление сообщение в правый верхний угол////////////////
let toastLiveSuccess = document.getElementById('successToast'); //выбор успешного сообщения
let toastLiveFailure = document.getElementById('failureToast');

//Функция успешного сообщения
let activateSuccessMessage = function () {
    let toast = new bootstrap.Toast(toastLiveSuccess);
    toast.show();
};
//функция ошибочного сообщения
let activateFailureMessage = function () {
    let toast = new bootstrap.Toast(toastLiveFailure);
    toast.show();
};

/*let addNewNoteFunc = function () { //Функция добавления записки
    let newPromise = fetch(`/MyApi/AddNewNote?Title=${}`, {
        method: 'POST',        
        credentials: "include",
        });
}*/

let newNoteButton = document.getElementById("addNewNote");
//newNoteButton.addEventListener("click", addNewNoteFunc);



//Модель создания записки
const modal = document.querySelector(".newNoteModal");
const closeButton = document.querySelector(".close-button");
const addButton = document.getElementById("addNewNoteButton");

function toggleModal() {
    modal.classList.toggle("show-newNoteModal");
    
}

function windowOnClick(event) {
    if (event.target === modal) {
        toggleModal();
    }
}

addButton.addEventListener("click", toggleModal);
closeButton.addEventListener("click", toggleModal);
window.addEventListener("click", windowOnClick);