<template>
  <header class="section">
    <div class="container grid items-center gap-6 md:grid-cols-2">
      <div class="space-y-6">
        <h1 class="heading-xl">One board. Zero repeated questions.</h1>
        <p class="text-body">Calm your family. Not your productivity.</p>
        <EmailCapture />
      </div>

      <aside class="card-glass space-y-4">
        <h2 class="heading-md">Executor peace of mind</h2>
        <ul class="space-y-3">
          <li class="flex items-start gap-3">
            <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
            <p class="text-body">Centralize documents, updates, and decisions in one calm workspace.</p>
          </li>
          <li class="flex items-start gap-3">
            <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
            <p class="text-body">Keep heirs aligned with audit-ready timelines and approvals.</p>
          </li>
          <li class="flex items-start gap-3">
            <span class="mt-1 h-2.5 w-2.5 rounded-full bg-gold-quiet"></span>
            <p class="text-body">Stay organized without extra calls, threads, or duplicate answers.</p>
          </li>
        </ul>
      </aside>
    </div>
  </header>
</template>

<script setup lang="ts">
import EmailCapture from '../../ui/EmailCapture.vue';
</script>
