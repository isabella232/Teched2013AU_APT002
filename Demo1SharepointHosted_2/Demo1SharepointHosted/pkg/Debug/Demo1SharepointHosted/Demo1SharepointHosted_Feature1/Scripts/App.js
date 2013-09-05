//'use strict';

var context;
var web;
var user;


var context = SP.ClientContext.get_current();
var user = context.get_web().get_currentUser();

// This code runs when the DOM is ready and creates a context object which is needed to use the SharePoint object model
$(document).ready(function () {
    getUserName();

    $("#getListCount").click(function (event) {
        getWebProperties();
        event.preventDefault();
    });
    $("#createlistbutton").click(function (event) {
        createlist();
        event.preventDefault();
    });
    $("#deletelistbutton").click(function (event) {
        deletelist();
        event.preventDefault();
    });

    welcome();
    displayLists();



});

// This function prepares, loads, and then executes a SharePoint query to get the current users information
function getUserName() {
    context.load(user);
    context.executeQueryAsync(onGetUserNameSuccess, onGetUserNameFail);
}

// This function is executed if the above call is successful
// It replaces the contents of the 'message' element with the user name
function onGetUserNameSuccess() {
    $('#message').text('Hello ' + user.get_title());
}

// This function is executed if the above call fails
function onGetUserNameFail(sender, args) {
    alert('Failed to get user name. Error:' + args.get_message());
}


function welcome() {
    // Get the user information, and try to load it into the current 
    // context.
    this.web = context.get_web();
    this.user = web.get_currentUser();
    this.context.load(user);
    this.context.executeQueryAsync(onUserReqSuccess, onUserReqFail);
}

function onUserReqSuccess() {
    // The current user information is loaded into the context – continue.
}

function onUserReqFail(sender, args) {
    // The current user information couldn’t be loaded into the context - display an 
    // error.
    alert('Failed to find current user. ' + args.get_message());
}

function getWebProperties() {
    // Get the number of lists in the current web.
    this.web = context.get_web();
    this.lists = this.web.get_lists();
    this.context.load(this.lists);
    this.context.executeQueryAsync(Function.createDelegate(this, this.onWebPropsSuccess), Function.createDelegate(this, this.onWebPropsFail));
}

function onWebPropsSuccess(sender, args) {
    alert('Number of lists in web: ' + this.lists.get_count());
}

function onWebPropsFail(sender, args) {
    alert('failed to get list. Error:' + args.get_message());
}

function displayLists() {
    // Get the available SharePoint lists, and then set them into 
    // the context.
    this.web = context.get_web();
    this.lists = this.web.get_lists();
    this.context.load(this.lists);
    this.context.executeQueryAsync(Function.createDelegate(this, this.onGetListsSuccess), Function.createDelegate(this, this.onGetListsFail));
}

function onGetListsSuccess(sender, args) {
    // Success getting the lists. Set references to the list 
    // elements and the list of available lists.
    var listEnumerator = this.lists.getEnumerator();
    var selectListBox = document.getElementById("selectlistbox");
    if (selectListBox.hasChildNodes()) {
        while (selectListBox.childNodes.length >= 1) {
            selectListBox.removeChild(selectListBox.firstChild);
        }
    }
    // Traverse the elements of the collection, and load the name of    
    // each list into the dropdown list box.
    while (listEnumerator.moveNext()) {
        var selectOption = document.createElement("option");
        selectOption.value = listEnumerator.get_current().get_title();
        selectOption.innerText = listEnumerator.get_current().get_title();
        selectListBox.appendChild(selectOption);
    }
}

function onGetListsFail(sender, args) {
    // Lists couldn’t be loaded - display error.
    alert('failed to get list. Error:' + args.get_message());
}

function createlist() {
    // Create a generic SharePoint list with the name that the user specifies.
    this.web = context.get_web();
    var listCreationInfo = new SP.ListCreationInformation();
    var listTitle = document.getElementById("createlistbox").value;
    listCreationInfo.set_title(listTitle);
    listCreationInfo.set_templateType(SP.ListTemplateType.genericList);
    this.lists = web.get_lists();
    var newList = this.lists.add(listCreationInfo);
    context.load(newList);
    context.executeQueryAsync(onListCreationSuccess, onListCreationFail);
}

function onListCreationSuccess() {
    displayLists();
}

function onListCreationFail(sender, args) {
    alert('Failed to create the list.' + args.get_message());
}

function deletelist() {
    // Delete the list that the user specifies.
    this.web = context.get_web();
    var selectListBox = document.getElementById("selectlistbox");
    var selectedListTitle = selectListBox.value;
    var selectedList = web.get_lists().getByTitle(selectedListTitle);
    selectedList.deleteObject();
    context.executeQueryAsync(onDeleteListSuccess, onDeleteListFail);
}

function onDeleteListSuccess() {
    displayLists();
}

function onDeleteListFail(sender, args) {
    alert('Failed to delete the list.' + args.get_message());
}
