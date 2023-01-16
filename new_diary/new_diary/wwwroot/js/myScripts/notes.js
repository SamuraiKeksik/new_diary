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
    let noteText = tinymce.activeEditor.getContent();
    let response = fetch(`/MyApi/UpdateNote?noteId=${currentNoteId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify({id: currentNoteId, text: noteText })
    });
    let result = response.then(response => console.log(response));

    //Изменение текста заметки в меню слева
    let currentNote = document.getElementById(currentNoteId);
    let currentNoteText = currentNote.querySelector(".note-text");
    //избавляемся от тегов и устанавливаем текст заметки
    currentNoteText.textContent = noteText.replace(/(<([^>]+)>)/ig, '');;

}


for (let note of notes) {
    note.addEventListener("click", changeActiveNote); //добавление функции к каждой записке
}
let firstNote = document.querySelector(".note-box"); //выбор первой записки
firstNote.click();


updateBtn.addEventListener("click", updateNote);

console.log(notes);
console.log(deleteBtn, updateBtn);


