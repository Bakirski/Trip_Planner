var bearerToken = localStorage.getItem("bearerToken");

window.onload = function () {
  console.log(window.location.pathname);
  getTrips();
};

async function getTrips() {
  try {
    const response = await fetch("http://localhost:5063/api/trips", {
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${bearerToken}`,
      },
    });

    const trips = await response.json();
    console.log(trips);
    displayData(trips);
  } catch (error) {
    console.error("Error fetching trips: ", error);
  }
}

function displayData(data) {
  const dataContainer = document.getElementById("dataContainer");

  data.forEach((item) => {
    const dataItem = document.createElement("tr");
    dataItem.classList.add("data-item");

    dataItem.innerHTML = `
      <td>${item.tripName}</td> 
      <td>${item.destination}</td> 
      <td>${
        item.description && item.description.trim()
          ? item.description
          : "No Description"
      }</td>
      <td>${item.startDate}</td>
      <td>${item.endDate}</td>
      <td><button value=${item.Id}>Update Trip</button></td>
      <td><button value=${item.Id}>Delete Trip</button></td>`;
    dataContainer.appendChild(dataItem);
  });
}

function toggleForm() {
  const displayForm = document.getElementById("createTripForm");
  displayForm.classList.toggle("hidden");
}

document
  .getElementById("createTripForm")
  .addEventListener("submit", async (event) => {
    event.preventDefault();

    const tripName = document.getElementById("tripName").value;
    const tripDestination = document.getElementById("destination").value;
    const tripDescription = document.getElementById("description").value;
    const startDate = document.getElementById("startDate").value;
    const endDate = document.getElementById("endDate").value;

    await createTrip(
      tripName,
      tripDestination,
      tripDescription,
      startDate,
      endDate
    );
    location.reload();
  });

async function createTrip(
  tripName,
  tripDestination,
  tripDescription,
  startDate,
  endDate
) {
  try {
    const response = await fetch("http://localhost:5063/api/trips", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${bearerToken}`,
      },
      body: JSON.stringify({
        TripName: tripName,
        Destination: tripDestination,
        Description: tripDescription,
        StartDate: startDate,
        EndDate: endDate,
      }),
    });
    const data = await response.json();
    console.log(data);
  } catch (error) {
    console.error("Error creating new trip: ", error);
  }
}
