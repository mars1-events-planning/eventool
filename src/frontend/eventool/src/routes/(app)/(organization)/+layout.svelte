<script lang="ts">
	import Star from '$lib/components/icons/Star.svelte';
	import Event from '$lib/components/icons/Event.svelte';
	import Person from '$lib/components/icons/Person.svelte';
	import SmallLogo from '$lib/components/icons/SmallLogo.svelte';
	import Footer from '$lib/components/Footer.svelte';
</script>

<div class="drawer lg:drawer-open">
	<input id="navbar-drawer" type="checkbox" class="drawer-toggle" />
	<div class="drawer-content flex flex-col pt-5 px-2 h-full">

		<slot />
		<div class="lg:hidden fixed inset-x-[50%] bottom-5">
			<label for="navbar-drawer" class="btn btn-circle btn-lg btn-primary drawer-button">
				<SmallLogo size={30} />
			</label>
		</div>
	</div>
	<div class="drawer-side">
		<label for="navbar-drawer" aria-label="close sidebar" class="drawer-overlay"></label>

		<div class="drawer-menu flex flex-col shadow-lg min-h-[95%] my-5 mx-5 rounded-2xl border pt-5 z-10">
			<label for="navbar-drawer" aria-label="close sidebar" class="drawer-close btn btn-ghost btn-circle absolute top-1 btn-sm right-1 text-xl font-bold">x</label>
			<ul class="menu p-4 w-80 text-base-content text-lg overflow-y-scroll overflow-x-scroll">
				<li>
					<a href="/list">
						<div class="pb-1">
							<Event size={20} />
						</div>
						<span>События </span>
					</a>
				</li>
				<li>
					<a href="/profile">
						<div class="pb-1">
							<Person size={20} />
						</div>
						<span>Профиль </span>
					</a>
				</li>
				<li>
					<a href="/" class="flex flex-row items-center">
						<div class="pb-1">
							<Star size={20} />
						</div>
						<span>Главная </span>
					</a>
				</li>
			</ul>
			<Footer />
		</div>
	</div>
</div>

<style lang="postcss">
	/* Adjust the drawer side to move it more to the left */
	@media (min-width: 1024px) {
		.drawer-close {
			display: none;
		}
	}

	@media (max-width: 1024px) {
		.drawer-toggle:not(:checked) ~ .drawer-side {
			transform: translateX(-20%);
		}

		.drawer-toggle:checked ~ .drawer-side > .drawer-menu {
			@apply bg-base-100;
		}

		.drawer-toggle:checked ~ .drawer-side > .drawer-overlay {
			@apply bg-transparent;
		}
	}

	@media (max-width: 768px) {
		.drawer-toggle:checked ~ .drawer-content .drawer-button {
			display: none;
		}
	}
</style>
