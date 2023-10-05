import axios from "axios";
process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";

const config = {
  apiUrlServerB: "https://localhost:7169/api",
  apiEndpoints: {
    Facility: "Facility",
    Resident: "Resident",
    ProgressNote: "ProgressNote",
  },
};

const syncData = async (endpoint) => {
  try {
    await axios.get(
      `${config.apiUrlServerB}/${endpoint}/sync-data-${endpoint}`
    );

    console.log(`Data ${endpoint} synced successfully!`);
  } catch (error) {
    console.error(`Error syncing data ${endpoint}:`, error.message);
  }
};

const syncAllData = async () => {
  const { apiEndpoints } = config;
  const endpointKeys = Object.keys(apiEndpoints);

  for (const endpoint of endpointKeys) {
    await syncData(apiEndpoints[endpoint]);
  }
};

// Set up a timer to run the syncAllData function every 10 seconds
setInterval(syncAllData, 5000);

// Start the initial sync immediately
syncAllData();
