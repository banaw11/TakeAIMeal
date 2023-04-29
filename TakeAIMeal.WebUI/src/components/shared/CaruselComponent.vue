<template>
    <div class="carusel" v-if="collection.length > 0">
        <div class="carusel-container">
            <div class="carusel-item" v-for="item in collection" :key="item.index">
                <span :class="{active : item.selected}">{{ item.value }}</span>
            </div>
        </div>
        <div class="carusel-navigation">
            <div class="carusel-navigation-item" v-for="item in collection" :key="item.index">
                <i class="dot" @click="goToIndex(item.index)" :class="{ active : item.selected}"></i>
            </div>
        </div>
    </div>
</template>

<script>
    import { defineComponent } from 'vue'
  export default defineComponent({
      name: 'CaruselComponent',
      props: ['data'],
      methods: {
          _loadCollection() {
              let collection = []
              if (this.data?.length > 0) {
                  collection = this.data.map((item, index) => {
                      return {
                          selected: index === 0,
                          index: index,
                          value: item
                      }
                  });
              }
              return collection;
          },
          _setCaruselInterval() {
              this.pooling = setInterval(() => {
                  const currentIndex = this.collection.findIndex((value) => {
                      return value.selected
                  });
                  if (this.collection.length - 1 > currentIndex) {
                      this.goToIndex(currentIndex + 1, false);
                  }
                  else {
                      this.goToIndex(0, false);
                  }
              },20000)
          },
          goToIndex(index, reset = true) {
              this.collection.forEach((value, i) => {
                  value.selected = index === i;
              })
              if (reset) {
                  clearInterval(this.pooling);
                  this._setCaruselInterval();
              }
          }
      },
      data() {
          return {
              collection: this._loadCollection()
          }
      },
      watch: {
          data: function(){
             this.collection = this._loadCollection();
          }
      },
      mounted: function () {
          this._setCaruselInterval();
      },
      beforeUnmount: function () {
          clearInterval(this.pooling);
      }
  })
</script>
