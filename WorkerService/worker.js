import axios from "axios";

const config = {
  apiUrlServerA: "https://localhost:7296/api",
  apiUrlServerB: "https://localhost:7169/api",
  apiEndpoints: {
    Facility: "Facility",
    Resident: "Resident",
    ProgressNote: "ProgressNote",
  },
};

const syncData = async (endpoint) => {
  try {
    const resA = await axios.get(
      `${config.apiUrlServerA}/${endpoint}/get-all-${endpoint}`
    );
    const dataA = resA.data;

    // Delete old data on ServerB
    await axios.delete(
      `${config.apiUrlServerB}/${endpoint}/delete-all-${endpoint}`
    );

    // Send dataA to API B
    const resB = await axios.post(
      `${config.apiUrlServerB}/${endpoint}/add-list-${endpoint}`,
      dataA
    );

    console.log(`Data ${endpoint} synced successfully!`, resB.data);
  } catch (error) {
    console.error(`Error syncing data ${endpoint}:`, error.message);
  }
};

const syncAllData = async () => {
  const { apiEndpoints } = config;
  const endpointKeys = Object.keys(apiEndpoints);

  await Promise.all(
    endpointKeys.map((endpoint) => syncData(apiEndpoints[endpoint]))
  );
};

// Set up a timer to run the syncAllData function every 10 seconds
setInterval(syncAllData, 10000);

// Start the initial sync immediately
syncAllData();
