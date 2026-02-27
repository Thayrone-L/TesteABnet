const apiUrl = "http://localhost:5049/Assignment";
document.addEventListener("DOMContentLoaded", () => {
    loadAssignments();
});
console.log("caiu aqui");
async function loadAssignments() {
    try {
        const response = await fetch(apiUrl);
        const data = await response.json();

        const list = document.getElementById("assignmentList");
        list.innerHTML = "";

        data.forEach(a => {
            const li = document.createElement("li");
            li.className = "list-group-item d-flex justify-content-between align-items-start";

            // badge de prioridade
            const priorityBadge = getPriorityBadge(a.priority);

            // se estiver atrasada, marca visualmente
            if (a.overdue) {
                li.classList.add("list-group-item-danger");
            }

            li.innerHTML = `
                <div>
                    <div class="fw-bold">${a.title}</div>
                    <small>${a.description ?? ""}</small><br>
                    <small>Due: ${a.dueDate ? new Date(a.dueDate).toLocaleDateString() : "Sem prazo"}</small>
                </div>
                ${priorityBadge}
            `;

            list.appendChild(li);
        });

    } catch (error) {
        console.error("Erro ao carregar tarefas:", error);
    }
}

function getPriorityBadge(priority) {
    switch (priority) {
        case 2:
            return `<span class="badge bg-danger">High</span>`;
        case 1:
            return `<span class="badge bg-warning text-dark">Medium</span>`;
        default:
            return `<span class="badge bg-secondary">Low</span>`;
    }
}