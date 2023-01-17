let notes = document.querySelectorAll(".note-box"); //список всех записок
let currentNoteId; //id выделенной записки
let updateBtn = document.querySelector(".update-btn");
let deleteBtn = document.querySelector(".delete-btn");
let noteTextarea = document.querySelector("#note-textarea"); 

let changeActiveNote = function (event) { //функция смены активной записки
        
    console.log("change");
    //Удаление класса active у всех записок
    for (let item of notes) {
        item.classList.remove("active");
    }
    //добавление класса к выбранной записке
    let noteBox = event.target.closest(".note-box");
    noteBox.classList.add("active");
    let noteId = noteBox.id;
    if (currentNoteId == noteId) //Если выбирается текущая записка - то возвращает ничего
        return;

    //Получение текста записки
    let promise = fetch(`/MyApi/GetNote?noteId=${noteId}`, { //отправка зарплата
        credentials: 'include'        
    });   
    promise.then(response => response.json())   //Обработка промиса
        .then(json => { //обработка JSON
            noteTextarea.value = json.text; //Изменение textarea до загрузки tinyMce
            tinymce.activeEditor.setContent(json.text);
        });
    currentNoteId = noteId; //Определение текущей записки
    
} 


//функция отправки записки на сервер
let updateNote = function () {
    console.log("Update");    
    let noteText = tinymce.activeEditor.getContent(); //Берем текст из редактора
    let response = fetch(`/MyApi/UpdateNote?noteId=${currentNoteId}`, { //отправляем текст на сервер
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify({id: currentNoteId, text: noteText })
    });
    response.then(response => {
        if (response.ok) {
            //изменение текста сообщения об успехе
            let currentNoteTitle = document.getElementById(currentNoteId).querySelector("h5").textContent;
            document.getElementById("successMessageText").innerText = `Note "${currentNoteTitle}" successfully updated!`;
            activateSuccessMessage();//Вывод сообщения
        }            
    });

    //Изменение текста заметки в меню слева
    let currentNote = document.getElementById(currentNoteId);
    let currentNoteText = currentNote.querySelector(".note-text");
    //избавляемся от тегов и устанавливаем текст заметки
    currentNoteText.textContent = noteText.replace(/(<([^>]+)>)/ig, ''); 
}


for (let note of notes) {
    note.addEventListener("click", changeActiveNote); //добавление функции к каждой записке
}
let firstNote = document.querySelector(".note-box"); //выбор первой записки
firstNote.click();

//добавление сообщение в правый верхний угол
let toastLiveSuccess = document.getElementById('liveToast')
let activateSuccessMessage = function () {
    let toast = new bootstrap.Toast(toastLiveSuccess);
    toast.show();
};



deleteBtn.addEventListener("click", function () {
    //Получение заголовка записки
    let currentNoteTitle = document.getElementById(currentNoteId).querySelector("h5").textContent;
    if (confirm(`Are you want to delete "${currentNoteTitle}" note?`)) //Если согласились на удаление записки, то удаляем её
    { 
        // Удаление записки
        let response = fetch(`/MyApi/DeleteNote?noteId=${currentNoteId}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json;charset=utf-8',
            },
        });
        response.then(response => {
            if (response.ok) {
                //изменение текста сообщения об успехе                
                document.getElementById("successMessageText").innerText = `Note "${currentNoteTitle}" successfully deleted!`;
                activateSuccessMessage();//Вывод сообщения
            }
        });          
    }
    else {
        // Если отменили удаление - то ничего не делается
    }
});



updateBtn.addEventListener("click", updateNote);
console.log(notes);
console.log(deleteBtn, updateBtn);


