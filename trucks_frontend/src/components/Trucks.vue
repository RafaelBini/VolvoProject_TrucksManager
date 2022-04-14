

<script>
const API_URL = `https://localhost:7163/api`;
const axios = require("axios").default;

export default {
  name: "AppTrucks",
  data() {
    return {
      trucks: [],
      models: [
        "FM 11",
        "FM 370",
        "FM 380",
        "FH 16",
        "FH 420",
        "FH 460",
        "FH 500",
        "FH 540",
      ],
      selectedTruck: null,
      lastSelectedId: null,
      hasPendigngChanges: null,
      errorMessage: "",
      modelYearRules: [
        (value) => !!value || "Required.",
        (value) =>
          (value && value == new Date().getFullYear()) ||
          value == new Date().getFullYear() + 1 ||
          "Must be current year or subsequent.",
      ],
    };
  },
  async created() {
    await this.loadTrucks();
    if (this.trucks.length > 0) this.select(this.trucks[0].id);
  },
  watch: {
    selectedTruck: {
      handler(truck) {
        const CHANGED_SELECTION = this.lastSelectedId != truck.id;
        const saveChanges = () => {
          this.hasPendigngChanges = true;
          if (this.selectedTruck.id) {
            axios
              .put(
                `${API_URL}/trucks/${this.selectedTruck.id}`,
                this.selectedTruck
              )
              .then(() => {
                this.loadTrucks();
              });
          } else {
            axios
              .post(`${API_URL}/trucks`, this.selectedTruck)
              .then((response) => {
                this.loadTrucks();
                this.selectedTruck = response.data;
              });
          }
          this.hasPendigngChanges = false;
          //this.loadTrucks();
          setTimeout(() => {
            this.hasPendigngChanges = null;
          }, 5000);
        };

        if (CHANGED_SELECTION) {
          this.lastSelectedId = truck.id;
        } else {
          this.errorMessage = "";

          if (!truck.modelName) {
            this.errorMessage = "Please enter a model name";
          } else if (
            truck.modelYear != new Date().getFullYear() + 1 &&
            truck.modelYear != new Date().getFullYear()
          ) {
            this.errorMessage =
              "The model year must be the current or subsequent ";
          } else if (truck.manufacturingYear != new Date().getFullYear()) {
            this.errorMessage =
              "The manufacturingYear year must be the current";
          } else {
            saveChanges();
          }
        }
      },
      deep: true,
    },
  },
  methods: {
    async loadTrucks() {
      this.trucks = await (await axios.get(`${API_URL}/trucks`)).data;
    },
    select(truckId) {
      const truck = this.trucks.find((t) => t.id == truckId);
      if (truck) this.selectedTruck = { ...truck };
    },
    del() {
      axios
        .delete(`${API_URL}/trucks/${this.selectedTruck.id}`)
        .then(async () => {
          await this.loadTrucks();
          if (this.trucks.length > 0) this.select(this.trucks[0].id);
          else this.selectedTruck = null;
        });
    },
    create() {
      this.selectedTruck = {
        modelName: "",
        modelYear: new Date().getFullYear(),
        manufacturingYear: new Date().getFullYear(),
      };
    },
  },
};
</script>

<template>
  <div class="container">
    <!-- List of trucks -->
    <div class="trucks-container">
      <div class="create-container">
        <v-btn @click="create()" color="primary">
          <v-icon>mdi-plus-thick</v-icon>
          NEW TRUCK
        </v-btn>
      </div>
      <div
        v-for="truck in trucks"
        :key="truck.id"
        @click="select(truck.id)"
        class="truck-selector"
        v-bind:class="{
          selected: selectedTruck && truck.id == selectedTruck.id,
        }"
      >
        {{ truck.modelName }} - {{ truck.modelYear }}
      </div>
    </div>

    <!-- Selected truck -->
    <div v-if="selectedTruck != null" class="selected-truck-container">
      <div class="status" v-bind:class="{ badStatus: errorMessage }">
        <span v-if="errorMessage && selectedTruck.id">
          {{ errorMessage }}
        </span>
        <span v-else-if="hasPendigngChanges != null">
          {{ hasPendigngChanges ? "Saving changes..." : "Saved changes" }}
        </span>
      </div>
      <div>
        <v-select
          v-model="selectedTruck.modelName"
          :items="models"
          label="Model"
        ></v-select>

        <div>
          <v-text-field
            label="Model Year"
            type="number"
            hide-details="auto"
            v-model="selectedTruck.modelYear"
            :rules="modelYearRules"
          ></v-text-field>
        </div>

        <div>
          <v-text-field
            label="Manufacturing Year"
            type="number"
            hide-details="auto"
            disabled
            v-model="selectedTruck.manufacturingYear"
          ></v-text-field>
        </div>
      </div>

      <div v-if="selectedTruck.id" class="buttons-container">
        <v-btn @click="del()" color="error"
          ><v-icon>mdi-delete</v-icon>Delete</v-btn
        >
      </div>
    </div>
    <div v-else class="selected-truck-container">
      <div class="message">
        No trucks registred!
        <v-icon> mdi-dump-truck </v-icon>
        <br />
        Click on <b>new truck</b> button to create
      </div>
    </div>
  </div>
</template>

<style scoped>
.container {
  display: flex;
  align-items: flex-start;
  justify-content: space-between;
  padding: 5px;
}
.trucks-container {
  padding: 7px;
  width: 50%;
}
.selected-truck-container {
  background-color: whitesmoke;
  border-radius: 6px;
  width: 50%;
  padding: 50px;
  position: relative;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-around;
}
.selected-truck-container div {
  margin-bottom: 20px;
}
.truck-selector {
  background-color: whitesmoke;
  border-radius: 6px;
  margin-bottom: 5px;
  padding: 12px;
  border: 1px solid transparent;
}
.truck-selector:hover {
  cursor: pointer;
  border: 1px dashed gray;
}
.selected {
  background-color: gray;
  color: white;
}
.status {
  color: gray;
  font-size: 11px;
  text-align: right;
  position: absolute;
  top: 5px;
  right: 5px;
}
.badStatus {
  color: red;
}
.buttons-container {
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: center;
  padding: 6px;
  width: 100%;
}
.create-container {
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 14px;
}
.message {
  padding: 30px;
  text-align: center;
  width: 100%;
}
</style>
