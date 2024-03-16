<script>
	import ProfileDrawer from './Drawers/Profile/ProfileDrawer.svelte';
	import ProfileModal from './Modals/ProfileModal.svelte';
	import SmallLogo from './icons/SmallLogo.svelte';
</script>

<div class="flex flex-col w-full shadow-[0px_5px_5px_5px] shadow-base-content/10 rounded-lg mt-5">
	<div class="navbar">
		<div class="navbar-start">
			<div class="dropdown">
				<div tabindex="0" role="button" class="btn btn-ghost text-primary">
					<SmallLogo size={25} />
				</div>
				<ul
					class="menu menu-sm dropdown-content mt-3 z-[2] p-2 shadow bg-base-100 rounded-box w-52"
				>
					<li><a href="/">Главная</a></li>
					<li><a href="/list">Мероприятия</a></li>
				</ul>
			</div>
		</div>
		<div class="navbar-end">
			<ProfileModal />
		</div>
	</div>
</div>
