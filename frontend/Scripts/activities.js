const tripId = sessionStorage.getItem("tripId");
const bearerToken = localStorage.getItem("bearerToken");

window.onload = function () {
  getActivities(tripId);
};

async function getActivities(id) {
  try {
    const tripResponse = await fetch(`http://localhost:5063/api/trips/${id}`, {
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${bearerToken}`,
      },
    });

    if (!tripResponse.ok) {
      const errorText = await tripResponse.text();
      throw new Error(
        errorText || `Trip request failed with status ${tripResponse.status}`
      );
    }

    const activitiesResponse = await fetch(
      `http://localhost:5063/api/trips/${id}/activities`,
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${bearerToken}`,
        },
      }
    );

    if (!activitiesResponse.ok) {
      const errorText = await activitiesResponse.text();
      throw new Error(
        errorText ||
          `Activities request failed with status ${activitiesResponse.status}`
      );
    }

    const tripData = await tripResponse.json();
    sessionStorage.setItem("tripId", tripData.id);

    const activitiesData = await activitiesResponse.json();
    console.log(tripData);
    console.log(activitiesData);

    var data =
      activitiesData && activitiesData.length > 0
        ? [tripData, activitiesData]
        : [tripData];

    displayData(data);
  } catch (error) {
    console.error("Error fetching data: ", error);
    alert(error.message);
  }
}

function displayData(data) {
  const activitiesContainer = document.getElementById("activitiesContainer");

  const tripTitle = document.getElementById("tripTitle");
  tripTitle.innerHTML = `Activities for "${data[0].tripName}"`;

  if (data.length > 1) {
    data[1].forEach((item) => {
      const dataItem = document.createElement("p");
      dataItem.innerHTML = `
    ${item.activityName}
    <button onclick="deleteActivity(${item.tripId}, ${item.id})">Delete</button>`;

      activitiesContainer.appendChild(dataItem);
    });
  } else null;
}

function toggleActivitiesForm() {
  document.getElementById("activitiesForm").classList.toggle("hidden");
}

document
  .getElementById("activitiesForm")
  .addEventListener("submit", async (event) => {
    event.preventDefault();

    const activityName = document.getElementById("activityName").value;
    createActivity(activityName);
  });

async function createActivity(activityName) {
  const id = sessionStorage.getItem("tripId");
  try {
    const response = await fetch(
      `http://localhost:5063/api/trips/${id}/activities`,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${bearerToken}`,
        },
        body: JSON.stringify({
          ActivityName: activityName,
        }),
      }
    );

    const data = await response.json();
    console.log(data);
    alert("Activity created successfully.");
    location.reload();
  } catch (error) {
    console.error("Error creating activity: ", error);
  }
}

async function deleteActivity(tripId, activityId) {
  try {
    const response = await fetch(
      `http://localhost:5063/api/trips/${tripId}/activities/${activityId}`,
      {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${bearerToken}`,
        },
      }
    );
    alert("Activity deleted successfully.");
    location.reload();
  } catch (error) {
    console.error("Error deleting activity: ", error);
  }
}
