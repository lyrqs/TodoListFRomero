const API = "http://localhost:5100/api/todolist";

document.addEventListener("DOMContentLoaded", () => {
  fetchCategories();

  document.getElementById("addItemForm").addEventListener("submit", async (e) => {
    e.preventDefault();
    const title = document.getElementById("title").value;
    const description = document.getElementById("description").value;
    const category = document.getElementById("category").value;

    await fetch(`${API}/add`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ title, description, category }),
    });

    e.target.reset();
    alert("Item added.");
  });

  document.getElementById("addProgressForm").addEventListener("submit", async (e) => {
    e.preventDefault();
    const id = +document.getElementById("itemId").value;
    const date = document.getElementById("date").value;
    const percent = +document.getElementById("percent").value;
  
    try {
      const res = await fetch(`${API}/progress`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ id, date, percent }),
      });
  
      if (!res.ok) {
        const errorText = await res.text();
        throw new Error(errorText || "Unknown error");
      }
  
      e.target.reset();
      alert("Progress added.");
    } catch (err) {
      alert(`Error adding progress: ${err.message}`);
    }
  });

  document.getElementById("refreshBtn").addEventListener("click", async () => {
    const res = await fetch(`${API}/items`);
    const items = await res.json();
  
    const container = document.getElementById("itemsContainer");
    container.innerHTML = "";

    const validCategories = ["Work", "Personal", "Shopping", "Urgent"];
  
    items.forEach(item => {
      const wrapper = document.createElement("div");
      wrapper.className = "todo-item";
  
      const header = document.createElement("h3");
      header.textContent = `${item.id}) ${item.title} - ${item.description} (${item.category}) Completed: ${item.isCompleted}`;
      wrapper.appendChild(header);
  
      const progressBar = document.createElement("div");
      progressBar.className = "progress-bar";
  
      const fill = document.createElement("div");
      const categoryClass = validCategories.includes(item.category) ? item.category : "Other";
      fill.className = `fill ${categoryClass}`;
      fill.style.width = `${item.totalProgress}%`;
      fill.textContent = `${item.totalProgress}%`;

      progressBar.appendChild(fill);
      wrapper.appendChild(progressBar);
  
      container.appendChild(wrapper);
    });
  });
});

async function fetchCategories() {
  const res = await fetch(`${API}/categories`);
  const categories = await res.json();
  const select = document.getElementById("category");
  select.innerHTML = "";

  categories.forEach(cat => {
    const opt = document.createElement("option");
    opt.value = cat;
    opt.textContent = cat;
    select.appendChild(opt);
  });
}
