const urlEditGet = `/Dependentes/EditGet/`;
const urlEditPost = `/Dependentes/EditPost/`;
const urlListar = '/Dependentes/ListaDependentes/' + document.getElementById('Id').value;
const urlCreate = '/Dependentes/Create/';
const urlDelete = `/Dependentes/Delete/`;

// PARTE REFERENTE AO POST

var dependentePost = {
}

let banco2 = []
retornarDependente();
function receberDependente() {
    var nomeDependente = document.getElementById('nome').value;
    var dataNascDependente = '1995-07-04';
    var parentescoDependente = document.getElementById('parentesco').value;
    var pessoaId = document.getElementById('Id').value;

    dependentePost = { "Nome": nomeDependente, "Parentesco": parentescoDependente, "DataNascimento": dataNascDependente, "PessoaId": pessoaId };
}

function addDependenteBanco() {
    receberDependente();
    fetch(urlCreate, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(dependentePost)
    }).then(res => {
        console.log("Request complete! response:", res);
        retornarDependente();
    });
}

// PARTE REFERENTE AO GET





function retornarDependente() {
    fetch(urlListar).then(response => response.json())
        .then(Dependentes => {
            banco2 = Dependentes
            console.log(banco2);
            atualizarTela();
        })
}

// OUTRO EXEMPLO

// fetch(url)
// .then(function(response){
//     return response.json();
// }).then(function(objeto)){
//     console.log(objeto.name);
// });


const limparDependentes = () => {
    const dependenteTabela = document.getElementById('dependenteTabela');
    while (dependenteTabela.firstChild) {
        dependenteTabela.removeChild(dependenteTabela.lastChild);
    }
}

function retirarDoBanco2(Id, Nome, DataNascimento, Parentesco) {
    const dependente = document.createElement('tr');
    dependente.classList.add('dependentes');
    dependente.innerHTML = `
                        <td class="td-nome-dependente">${Nome}</td>
                        <td class="td-dataNasc">${DataNascimento}</td>
                        <td class="td-dependente">${Parentesco}</td>
                        <td > <button type="button" class="btn btn-danger" onclick="apagaDependente('${Id}')">Deletar</button> </td>
                        <td>  <button type="button" class="btn btn-primary" onclick="editarDependenteGet('${Id}')" data-toggle="modal" 
                        data-target="#modalEditarDependente">Editar</button> </td>
                        `
    document.getElementById('dependenteTabela').appendChild(dependente);
}

const atualizarTela = () => {
    limparDependentes();
    banco2.forEach(item => retirarDoBanco2(item.id, item.nome, item.dataNascimento, item.parentesco));
}



function limpar() {
    document.getElementById('nome').value = '';
    document.getElementById('parentesco').value = '';
}



// ARTE REFERENTE AO DELETE

async function apagaDependente(id) {
    await fetch(urlDelete + id, {
        method: 'DELETE',
    })
        .then(res => res.text());
    retornarDependente();
}

// ARTE REFERENTE AO EDITAR

// function editarDependente(id) {
//     const addNomeModal = document.getElementById('nome-editar');
//     const addParentescoModal = document.getElementById('parentesco-editar');


//     const dependente = {
//         "Id": addNomeModal.value.trim(),
//         "Parentesco": addParentescoModal.value.trim(),
//     };

//     fetch(urlEdit + id, {
//         method: 'PUT',
//         body: Json.stringify(dependente)
//     })
//         .then(res => res.text()) // or res.json()
//         .then(res => console.log(res))

// }

// GET DO EDIT

function editarDependenteGet(id) {
    fetch(urlEditGet + id, {
        method: 'GET',
    })
        .then(res => res.json()) // or res.json()
        .then(data => popularModal(data))
}

function popularModal(data) {
    document.getElementById('nome-editar').value = data.nome;
    document.getElementById('parentesco-editar').value = data.parentesco;
    document.getElementById('data-editar').value = data.dataNascimento;
    document.getElementById('id-editar').value = data.id;

}

// POST DO EDIT

function editarDependentePut() {
    const nomeDependente = document.getElementById('nome-editar').value;
    const dataNascDependente = document.getElementById('data-editar').value;
    const parentesco = document.getElementById('parentesco-editar').value;
    const pessoaId = document.getElementById('Id').value;
    const dependenteId = document.getElementById('id-editar').value;

    const dependente = {
        "Id": dependenteId.value,
        "Nome": nomeDependente.value.trim(),
        "Parentesco": parentesco.value.trim(),
        "DataNascimento": dataNascDependente.value.trim(),
        "PessoaId": pessoaId.value.trim(),
    }

    fetch(urlEditPost + dependenteId, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(dependente)
    }).then(res => {
        console.log("Request complete! response:", res);
        retornarDependente();
    });

}





