<script>
	import Logo from '$lib/components/icons/Logo.svelte';
</script>

<svelte:head>
	<title>Eventool</title>
</svelte:head>

<div class="flex flex-col items-center justify-center h-[100vh] gap-20">
	<div class="text-primary">
		<Logo size={750} />
	</div>
	<div class="flex flex-row space-x-10 items-center">
		<a class="btn  btn-ghost btn-lg" href="/register">Регистрация</a>
		<a class="btn btn-neutral btn-lg" href="/login">Вход</a>
	</div>
</div>
