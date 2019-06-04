document.getElementById("Name").addEventListener("click", resetNameAlert);
document.getElementById("MobilePhone").addEventListener("click", resetMobilePhoneAlert);
document.getElementById("BirthDate").addEventListener("click", resetBirthDateAlert);
function validateDate(date) {
    if (date.length > 0) {
        var dob =date;
        var datesplited = dob.split(".");
        if (isNaN(Date.parse(datesplited[2] + "-" + datesplited[1] + "-" + datesplited[0]))) {
            return false;
        }
        return true;
        //var ValidateDatePattern = /^(\d{2})\.(\d{2})\.(\d{4})$/;

        //console.log(ValidateDatePattern.test(date));
        //return ValidateDatePattern.test(date);
    }
    return true;
}
function validateName(name) {
    if (name.length > 0) {
        var ValidateNamePattern = /^[A-ZА-Я]([A-zА-я]+)(([\s?\-?]){1}[A-zА-я]+){0,5}$/;
        return ValidateNamePattern.test(name);
    }
    return false;
}
function validateNumber(number) {
    if (number.length > 0) {
        var ValidateNumberPattern = /^\+?\d{12}$/;
        return ValidateNumberPattern.test(number);
    }
    return true;
}
function resetNameAlert() {
    document.getElementById("Name").classList.remove("border-danger");
}
function resetMobilePhoneAlert() {
    document.getElementById("MobilePhone").classList.remove("border-danger");

}
function resetBirthDateAlert() {
    document.getElementById("BirthDate").classList.remove("border-danger");

}
function clearAlertMessages() {
    document.getElementById("FormNameError").innerHTML = "";
    document.getElementById("FormDateError").innerHTML = "";
    document.getElementById("FormNumberError").innerHTML = "";
}
function validateForm() {
    var flagError = false;
    clearAlertMessages();


    var ContactPhone = document.forms["contactForm"]["MobilePhone"].value;
    var ContactBirthDate = document.forms["contactForm"]["BirthDate"].value;
    var ContactName = document.forms["contactForm"]["Name"].value;

    if (!validateName(ContactName)) {
        document.getElementById("FormNameError").innerHTML = `Поле не должно быть пустым.</br>
                                                                  Имя должно начинаться с большой буквы и не содержать в себе цифр.</br>
                                                                  Разрешены составные имена, в которых части
                                                                  отделены знаком пробела или дефисом(до 6 частей).`;
        document.getElementById("Name").classList.add("border-danger");
        flagError = true;
    }

    if (!validateDate(ContactBirthDate)) {
        document.getElementById("FormDateError").innerHTML = "Введите дату в формате dd.mm.yyyy";
        document.getElementById("BirthDate").classList.add("border-danger");
        flagError = true;
    }
    if (!validateNumber(ContactPhone)) {
        document.getElementById("FormNumberError").innerHTML = "Введите номер в международном формате (+XXXXXXXXXXXX)";
        document.getElementById("MobilePhone").classList.add("border-danger");
        flagError = true;
    }
    
    if (flagError) {
        return false;
    }

    else
        return true;
}