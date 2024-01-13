import pandas as pd


def format_number(num):
    for unit in ['', 'K', 'M', 'B', 'T']:
        if abs(num) < 1000.0:
            return "%3.1f%s" % (num, unit)
        num /= 1000.0
    return "%.1f%s" % (num, 'P')


def armor_multiplier(level):
    if level < 10:
        return 1
    elif level < 20:
        return 2
    elif level < 30:
        return 4
    elif level < 40:
        return 6
    elif level < 50:
        return 8
    elif level < 60:
        return 10
    elif level < 70:
        return 12
    elif level < 80:
        return 14
    elif level < 90:
        return 16
    else:
        return 18


def calculate_weapon_damage():
    base_damage = 100
    quality_multipliers = {
        'Poor': 1,
        'Uncommon': 2,
        'Rare': 3,
        'Epic': 4,
        'Legendary': 5
    }
    data = []
    for quality, multiplier in quality_multipliers.items():
        for level in range(1, 101):
            level_multiplier = 1 + ((level - 1) * 0.42) ** 6
            weapon_damage = multiplier * base_damage * level_multiplier * armor_multiplier(level)
            data.append({'Quality': quality, 'Level': level, 'Weapon Damage': format_number(weapon_damage)})
    df = pd.DataFrame(data)
    df_pivot = df.pivot(index='Level', columns='Quality', values='Weapon Damage')
    df_pivot = df_pivot[['Poor', 'Uncommon', 'Rare', 'Epic', 'Legendary']]
    return df_pivot


def calculate_monster_hp():
    base_hp = 100
    data = []
    for level in range(1, 101):
        level_multiplier = 1 + ((level - 1) * 0.29) ** 7
        monster_hp = base_hp * level_multiplier * 28 * armor_multiplier(level)
        data.append({'Level': level, 'Monster HP': format_number(monster_hp)})
    df = pd.DataFrame(data)
    return df


# Calculate weapon damage and monster HP
df_weapon = calculate_weapon_damage()
df_monster = calculate_monster_hp()

# Write both dataframes to the same Excel file, each on a different sheet
with pd.ExcelWriter('combined_data.xlsx') as writer:
    df_weapon.to_excel(writer, sheet_name='Weapon Damage')
    df_monster.to_excel(writer, sheet_name='Monster HP', index=False)
