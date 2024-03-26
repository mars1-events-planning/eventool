<script>
	import { quintOut } from "svelte/easing";
	import { fly } from "svelte/transition";
</script>
<div
	role="alert"
	class="alert alert-success text-white w-fit self-center mt-5"
	transition:fly={{ delay: 0, duration: 100, x: 100, y: 500, opacity: 0.5, easing: quintOut }}
>
	<svg
		xmlns="http://www.w3.org/2000/svg"
		class="stroke-current shrink-0 h-6 w-6"
		fill="none"
		viewBox="0 0 24 24"
		><path
			stroke-linecap="round"
			stroke-linejoin="round"
			stroke-width="2"
			d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z"
		/></svg
	>
	<span>Изменения успешно сохранены!</span>
</div>
