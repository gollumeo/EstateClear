<template>
  <main class="bg-ivory">
    <HeroSection />
    <HowItWorks />
    <WhyItMatters />

    <section class="section">
      <div class="container space-y-6">
        <p class="heading-md text-gold-quiet uppercase tracking-wide">ExecutorBoard</p>
        <h2 class="heading-lg">Quiet luxury for estate executors</h2>
        <p class="text-body">
          A calm, sovereign workspace for attorneys and executors to keep heirs informed, decisions traceable,
          and estates moving without friction.
        </p>
        <div class="flex flex-wrap gap-3">
          <RouterLink class="btn-primary" to="/for-attorneys">Talk with our team</RouterLink>
          <RouterLink class="btn-secondary btn-sm" to="/guides/how-to-keep-heirs-updated">Read the guide</RouterLink>
        </div>
      </div>
    </section>
  </main>
</template>

<script setup lang="ts">
import HeroSection from '../modules/hero/HeroSection.vue';
import HowItWorks from '../modules/howitworks/HowItWorks.vue';
import WhyItMatters from "../modules/why/WhyItMatters.vue";
</script>
