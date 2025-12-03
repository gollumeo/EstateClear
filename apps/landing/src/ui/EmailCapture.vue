<template>
  <form class="flex flex-col gap-3 sm:flex-row sm:items-center" @submit.prevent="onSubmit">
    <label class="sr-only" for="email">Email</label>
    <input
      id="email"
      v-model="email"
      type="email"
      class="input sm:flex-1"
      name="email"
      placeholder="you@firm.com"
      autocomplete="email"
      required
    />
    <button type="submit" class="btn-primary w-full sm:w-auto">Join the waitlist</button>
  </form>
</template>

<script setup lang="ts">
import { ref } from 'vue';

const email = ref('');

const onSubmit = () => {
  const value = email.value.trim();
  console.log('ExecutorBoard early access request:', value);
};
</script>
