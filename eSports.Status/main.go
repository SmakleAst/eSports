package main

import (
	"encoding/json"
	"fmt"
	"net/http"

	"github.com/gorilla/mux"
	"github.com/rs/cors"
)

type ServiceStatus struct {
	ServiceName string `json:"serviceName"`
	Status      string `json:"status"`
	Message     string `json:"message,omitempty"`
}

func main() {
	router := mux.NewRouter()
	router.HandleFunc("/status", statusHandler).Methods("GET")

	corsHandler := cors.Default().Handler(router)

	fmt.Println("Status service is running on port 8081")
	http.ListenAndServe(":8081", corsHandler)
}

func statusHandler(w http.ResponseWriter, r *http.Request) {
	playersStatus := getServiceStatus("Players Service", "https://localhost:7160/Player/GetStatus")
	teamsStatus := getServiceStatus("Teams Service", "https://localhost:7246/Team/GetStatus")
	statsStatus := getServiceStatus("Stats Service", "https://localhost:7126/Stats/GetStatus")
	tournamentsStatus := getServiceStatus("Tournaments Service", "https://localhost:7171/Tournament/GetStatus")

	statuses := []ServiceStatus{playersStatus, teamsStatus, statsStatus, tournamentsStatus}
	jsonData, err := json.Marshal(statuses)
	if err != nil {
		http.Error(w, "Internal Server Error", http.StatusInternalServerError)
		return
	}

	w.Header().Set("Access-Control-Allow-Origin", "http://localhost:5173")
	w.Header().Set("Content-Type", "application/json")
	w.WriteHeader(http.StatusOK)
	w.Write(jsonData)
}

func getServiceStatus(serviceName, serviceURL string) ServiceStatus {
	resp, err := http.Get(serviceURL)
	if err != nil {
		return ServiceStatus{ServiceName: serviceName, Status: "Not OK", Message: err.Error()}
	}
	defer resp.Body.Close()

	if resp.StatusCode == http.StatusOK {
		return ServiceStatus{ServiceName: serviceName, Status: "OK"}
	}

	return ServiceStatus{ServiceName: serviceName, Status: "Not OK", Message: fmt.Sprintf("HTTP Status %d", resp.StatusCode)}
}
