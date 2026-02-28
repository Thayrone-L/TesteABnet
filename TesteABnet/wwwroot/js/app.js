document.addEventListener("DOMContentLoaded", loadAssignments);

let createModal;
let isEditing = false;
let editingId = null;

document.addEventListener("DOMContentLoaded", () => {
    const modalElement = document.getElementById("createModal");
    createModal = new bootstrap.Modal(modalElement);
    loadAssignments();
});

async function loadAssignments() {
    try {
        const response = await fetch("http://localhost:5049/Assignment");

        if (!response.ok) {
            throw new Error("Erro ao buscar tarefas");
        }

        const data = await response.json();

        const container = document.getElementById("assignmentList");
        container.innerHTML = "";

        if (!data || data.length === 0) {
            container.innerHTML = `
                <div class="alert alert-info">
                    Nenhuma tarefa encontrada.
                </div>
            `;
            return;
        }

        data.forEach((a, index) => {

            const priorityBadge = getPriorityBadge(a.priority);

            const overdueBadge = a.overdue
                ? `<span class="badge bg-danger ms-2">Overdue</span>`
                : "";

            const dueDateFormatted = a.dueDate
                ? new Date(a.dueDate).toLocaleDateString()
                : "Sem prazo";

            const accordionItem = document.createElement("div");
            accordionItem.className = "accordion-item";

            accordionItem.innerHTML = `
    <h2 class="accordion-header" id="heading${index}">
        <div class="d-flex justify-content-between align-items-center">

            <button class="accordion-button collapsed flex-grow-1 text-start"
                    type="button"
                    data-bs-toggle="collapse"
                    data-bs-target="#collapse${index}"
                    aria-expanded="false"
                    aria-controls="collapse${index}">

                <div class="d-flex align-items-center gap-2">
                    <strong>${a.title}</strong>
                    ${overdueBadge}
                    ${priorityBadge}
                </div>
            </button>

            <div class="ms-2 d-flex gap-2 pe-3">
                <button class="btn btn-sm btn-outline-primary"
                        onclick="editAssignment(${a.id})">
                    Editar
                </button>

                <button class="btn btn-sm btn-outline-danger"
                        onclick="deleteAssignment(${a.id})">
                    Excluir
                </button>
            </div>

        </div>
    </h2>

    <div id="collapse${index}"
         class="accordion-collapse collapse"
         aria-labelledby="heading${index}">

        <div class="accordion-body">
            <p class="mb-2">
                <strong>Descrição:</strong><br>
                ${a.description ?? "Sem descrição"}
            </p>
            <p class="mb-0">
                <strong>Due Date:</strong> ${dueDateFormatted}
            </p>
        </div>
    </div>
`;

            container.appendChild(accordionItem);
        });

    } catch (error) {
        console.error("Erro:", error);

        const container = document.getElementById("assignmentList");
        container.innerHTML = `
            <div class="alert alert-danger">
                Erro ao carregar tarefas.
            </div>
        `;
    }
}

function getPriorityBadge(priority) {
    switch (priority) {
        case 2:
            return `<span class="badge bg-danger">HIGH</span>`;
        case 1:
            return `<span class="badge bg-warning text-dark">MEDIUM</span>`;
        case 0:
            return `<span class="badge bg-secondary">LOW</span>`;
        default:
            return `<span class="badge bg-light text-dark">UNKNOWN</span>`;
    }
}

function openCreateModal() {
    isEditing = false;
    editingId = null;

    document.getElementById("assignmentForm").reset();

    const modal = new bootstrap.Modal(document.getElementById("assignmentModal"));
    modal.show();
}

async function createAssignment() {

    const assignment = {
        title: document.getElementById("title").value,
        description: document.getElementById("description").value,
        priority: parseInt(document.getElementById("priority").value),
        status: 0,
        dueDate: document.getElementById("dueDate").value || null
    };

    try {
        const response = await fetch("http://localhost:5049/Assignment", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(assignment)
        });

        if (!response.ok) {
            throw new Error("Erro ao criar tarefa");
        }

        // Fecha modal
        createModal.hide();

        // Limpa formulário
        document.getElementById("createForm").reset();

        // Atualiza lista
        loadAssignments();

    } catch (error) {
        console.error(error);
        alert("Erro ao salvar task.");
    }
}

async function editAssignment(id) {
    try {
        const response = await fetch(`http://localhost:5049/Assignment/${id}`);

        if (!response.ok)
            throw new Error("Erro ao buscar tarefa");

        const data = await response.json();

        isEditing = true;
        editingId = id;

        // Preenche campos
        document.getElementById("title").value = data.title;
        document.getElementById("description").value = data.description ?? "";
        document.getElementById("priority").value = data.priority;
        document.getElementById("status").value = data.status;
        document.getElementById("dueDate").value = data.dueDate
            ? data.dueDate.split("T")[0]
            : "";

        const modal = new bootstrap.Modal(document.getElementById("assignmentModal"));
        modal.show();

    } catch (error) {
        console.error(error);
    }
}

async function saveAssignment() {
    const assignment = {
        title: document.getElementById("title").value,
        description: document.getElementById("description").value,
        priority: parseInt(document.getElementById("priority").value),
        status: parseInt(document.getElementById("status").value),
        dueDate: document.getElementById("dueDate").value || null
    };

    let url = "http://localhost:5049/Assignment";
    let method = "POST";

    if (isEditing) {
        url += `/${editingId}`;
        method = "PUT";
        assignment.id = editingId;
    }

    const response = await fetch(url, {
        method: method,
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(assignment)
    });

    if (!response.ok)
        throw new Error("Erro ao salvar");

    const modalElement = document.getElementById("assignmentModal");
    const modal = bootstrap.Modal.getInstance(modalElement);
    modal.hide();

    await loadAssignments();
}

async function deleteAssignment(id) {

    if (!confirm("Tem certeza que deseja excluir esta task?"))
        return;

    try {
        const response = await fetch(`http://localhost:5049/Assignment/${id}`, {
            method: "DELETE"
        });
         
        if (!response.ok) {
            throw new Error("Erro ao deletar");
        }

        loadAssignments();

    } catch (error) {
        console.error(error);
        alert("Erro ao excluir task.");
    }
}