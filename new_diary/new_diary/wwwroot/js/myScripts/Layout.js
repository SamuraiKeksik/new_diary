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

let newNoteButton = document.getElementById("addNewNote");

//Модель создания записки
const modal = document.querySelector(".newNoteModal");
const closeButton = document.querySelector(".close-button");
const addButton = document.getElementById("addNewNoteButton");

function toggleModal() { 
    modal.classList.toggle("show-newNoteModal"); //включает и выключает модальное окно
    
}

function windowOnClick(event) {
    if (event.target === modal) {
        toggleModal(); //Если нажимаем на фон, то модальное окно закрывается
    }
}

addButton.addEventListener("click", toggleModal); 
closeButton.addEventListener("click", toggleModal);
window.addEventListener("click", windowOnClick);

let createNoteForm = document.getElementById("createNoteForm"); //выбираем форму создания заметки
    createNoteForm.addEventListener('submit', function (e) {
    e.preventDefault(); //Предотвращает стандартное поведение
    const newNoteTitle = document.getElementById("newNoteTitle").value; //Заголовок записки
    let response = fetch('/MyApi/CreateNote', { //посылает запрос на сервер
        method: 'POST',
        credentials: "include",
        headers: {
            'Content-Type': 'application/json;charset=UTF-8',
        },
        body: JSON.stringify(newNoteTitle),
        
    });

        toggleModal();
        document.getElementById("successMessageText").innerText = `Note "${newNoteTitle}" successfully created!`;
        activateSuccessMessage();//Вывод сообщения
})